using CoffeePalace.Models.Common;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public interface ICoffeeProductFactory : IFactory<CoffeeProduct>
{
    public ICoffeeProductFactory SetName(string name);

    public ICoffeeProductFactory SetPrice(decimal price);

    public ICoffeeProductFactory SetCountryOfOrigin(string countryOfOrigin);

    public ICoffeeProductFactory SetDescription(string description);

    public ICoffeeProductFactory SetIsInStock(bool isStock);

    public ICoffeeProductFactory SetRoastLevel(RoastLevelType roastLevel);

    public ICoffeeProductFactory SetCaffeineContent(CaffeineContentType caffeineContent);

    public ICoffeeProductFactory SetBeanType(BeanType beanType);

    public ICoffeeProductFactory SetGrindType(GrindType grindType);
}