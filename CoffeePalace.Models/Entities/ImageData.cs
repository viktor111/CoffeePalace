using CoffeePalace.Models.Common;

namespace CoffeePalace.Models.Entities;

public class ImageData : Entity
{
    public string Name { get; set; }
    
    public byte[] Data { get; set; }
    
    public byte[]? Thumbnail { get; set; }
    
    public byte[]? FullScreen { get; set; }
    
    public string ExternalId { get; set; }
}   