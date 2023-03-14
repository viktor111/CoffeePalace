using CoffeePalace.Models.Common;

namespace CoffeePalace.Models.Entities;

public class BasketItem : Entity
{
    public string BasketId { get; set; }
    
    public Basket Basket { get; set; }
    
    public string CoffeeProductId { get; set; }
    
    public CoffeeProduct CoffeeProduct  { get; set; }
    
    public int Quantity { get; set; }
}