using System.Reflection;
using CoffeePalace.Models.Entities;

namespace CoffeePalace.Models.Factories;

public class ImageDataFactory : IImageDataFactory
{
    private string name = default!;
    private byte[] data = default!;
    private string externalId = default!;

    private bool isNameSet = false;
    private bool isDataSet = false;
    private bool isExternalIdSet = false;
    
    public IImageDataFactory SetName(string name)
    {
        this.name = name;
        isNameSet = true;

        return this;
    }

    public IImageDataFactory SetData(byte[] data)
    {
        this.data = data;
        isDataSet = true;

        return this;
    }

    public IImageDataFactory SetExternalId(string externalId)
    {
        this.externalId = externalId;
        isExternalIdSet = true;

        return this;
    }
    
    public ImageData Create()
    {
        if (!this.isNameSet) throw new Exception("Name must be set");
        if (!this.isDataSet) throw new Exception("Data must be set");
        if (!this.isExternalIdSet) throw new Exception("ExternalId must be set");

        return new ImageData
        {
            Name = this.name,
            Data = this.data,
            ExternalId = this.externalId
        };
    }
}