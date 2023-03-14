using CoffeePalace.Models.Common;

namespace CoffeePalace.Models.Entities;

public class Basket : Entity
{
    public string UserId { get; set; }
    
    public ICollection<BasketItem> BasketItems { get; set; }
}