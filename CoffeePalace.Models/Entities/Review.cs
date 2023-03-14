using CoffeePalace.Models.Common;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Entities;

public class Review : Entity
{
    public string Text { get; set; }

    public RatingType Rating { get; set; }
    
    public string UserId { get; set; }
    
    public User User { get; set; }
    
    public string CoffeeProductId { get; set; }
    
    public CoffeeProduct CoffeeProduct { get; set; }
}