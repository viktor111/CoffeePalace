using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace CoffeePalace.Services;

public class ImageProcessingService : IImageProcessingService
{   
    private const int THUMBNAIL_WIDTH = 300;
    private const int FULLSCREEN_WIDTH = 1000;
    
    public async Task<Result<ImageData>> Process(ImageData imageData)
    {
        var stream = new MemoryStream(imageData.Data);
        var image = await Image.LoadAsync(stream);

        var thumbnailData = await CreateResizedJpeg(image, THUMBNAIL_WIDTH);
        var fullscreenData = await CreateResizedJpeg(image, FULLSCREEN_WIDTH);

        return new ImageData
        {
            Name = imageData.Name,
            Data = imageData.Data,
            Thumbnail = thumbnailData,
            FullScreen = fullscreenData,
            ExternalId = imageData.ExternalId
        };
    }

    private async Task<byte[]> CreateResizedJpeg(Image image, int resizeWidth)
    {
        var width = image.Width;
        var height = image.Height;

        if (width > resizeWidth)
        {
            height = (int)((double)resizeWidth / width * height);
            width = resizeWidth;
        }

        image
            .Mutate(i => i
                .Resize(new Size(width, height)));

        image.Metadata.ExifProfile = null;

        var memoryStream = new MemoryStream();


        await image.SaveAsJpegAsync(memoryStream, new JpegEncoder
        {
            Quality = 75
        });

        return memoryStream.ToArray();
    }
}