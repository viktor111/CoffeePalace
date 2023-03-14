using CoffeePalace.Models.Common;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Entities;

public class CoffeeProduct : Entity
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string CountryOfOrigin { get; set; }
    
    public string Description { get; set; }
    
    public bool IsInStock { get; set; }
    
    public RoastLevelType RoastLevel { get; set; }
    
    public CaffeineContentType CaffeineContent { get; set; }
    
    public BeanType BeanType { get; set; }
    
    public GrindType GrindType { get; set; }
    
    public IEnumerable<Review> Reviews { get; set; }
    
    public IEnumerable<BasketItem> BasketItems { get; set; }
}