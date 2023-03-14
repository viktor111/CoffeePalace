using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface IImageProcessingService
{
    public Task<Result<ImageData>> Process(ImageData imageData);
}