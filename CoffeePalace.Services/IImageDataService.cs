using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface IImageDataService
{
    public Task<Result<ImageData>> Save(ImageData imageData);

    public Task<Result<ImageData>> Update(string id, ImageData imageData);
    
    public Task<Result> Delete(string id);
    
    public Task<Result<IEnumerable<ImageData>>> All();

    public Task<Result<ImageData>> GetById(string id);

    public Task<Result<ImageData>> GetByExternalId(string externalId);
}