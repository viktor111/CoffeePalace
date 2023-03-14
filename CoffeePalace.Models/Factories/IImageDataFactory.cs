using CoffeePalace.Models.Common;
using CoffeePalace.Models.Entities;

namespace CoffeePalace.Models.Factories;

public interface IImageDataFactory : IFactory<ImageData>
{
    public IImageDataFactory SetName(string name);

    public IImageDataFactory SetData(byte[] data);

    public IImageDataFactory SetExternalId(string externalId);
}