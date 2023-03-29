using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using CoffeePalace.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class ImageDataService : IImageDataService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ImageDataService> logger;
    private readonly IImageProcessingService imageProcessingService;

    public ImageDataService(
        ApplicationDbContext dbContext,
        ILogger<ImageDataService> logger,
        IImageProcessingService imageProcessingService)
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.imageProcessingService = imageProcessingService;
    }

    public async Task<Result<ImageData>> Save(ImageData imageData)
    {
        try
        {
            var isValid = ImageDataValidator.Validate(imageData);
            if (!isValid.Succeeded) return isValid.Errors.First();
            
            var processedImage = await this.imageProcessingService.Process(imageData);

            if (!processedImage.Succeeded) return processedImage.Errors.First();

            var result = await this.dbContext.ImageDatas.AddAsync(imageData);

            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}",e);
            return ErrorMessageBuilder.Save(nameof(ImageData));
        }
    }

    public async Task<Result<ImageData>> Update(string id, ImageData imageData)
    {
        try
        {
            var isValid = ImageDataValidator.Validate(imageData);
            if (!isValid.Succeeded) return isValid.Errors.First();
            
            var old = await this.dbContext.ImageDatas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (old is null) return ErrorMessageBuilder.NotFound(nameof(ImageData));

            var processed = await this.imageProcessingService.Process(imageData);

            if (!processed.Succeeded) return ErrorMessageBuilder.Update(nameof(ImageData), "processing was not a success");

            old.Name = processed.Data.Name;
            old.Data = processed.Data.Data;
            old.Thumbnail = processed.Data.Thumbnail;
            old.FullScreen = processed.Data.FullScreen;
            
            await this.dbContext.SaveChangesAsync();

            return old;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}",e);
            return ErrorMessageBuilder.Update(nameof(ImageData));
        }
    }

    public async Task<Result> Delete(string id)
    {
        try
        {
            var imageData = await this.dbContext.ImageDatas
                .FirstOrDefaultAsync(x => x.Id == id);

            if (imageData is null) return ErrorMessageBuilder.NotFound(nameof(ImageData));

            this.dbContext.ImageDatas.Remove(imageData);

            await this.dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}",e);
            return ErrorMessageBuilder.Delete(nameof(ImageData));
        }
    }

    public async Task<Result<IEnumerable<ImageData>>> All()
    {
        try
        {
            var images = await this.dbContext.ImageDatas.ToArrayAsync();

            return images;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}",e);
            return ErrorMessageBuilder.All(nameof(ImageData));
        }
    }

    public async Task<Result<ImageData>> GetById(string id)
    {
        try
        {
            var imageData = await this.dbContext.ImageDatas.FirstOrDefaultAsync(x => x.Id == id);

            if (imageData == null)
                return ErrorMessageBuilder.Get(nameof(ImageData));

            return imageData;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Get(nameof(ImageData));
        }
    }

    public async Task<Result<ImageData>> GetByExternalId(string externalId)
    {
        try
        {
            var imageData = await this.dbContext.ImageDatas.FirstOrDefaultAsync(x => x.ExternalId == externalId);

            if (imageData == null)
                return ErrorMessageBuilder.Get(nameof(ImageData));

            return imageData;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Get(nameof(ImageData));
        }
    }
}