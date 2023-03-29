using Bogus;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using FakeItEasy;

using static CoffeePalace.Models.Constants.CoffeeProductConstants;

namespace CoffeePalace.Tests.Fakes;

public class CoffeeProductFakes
{
    public class CoffeeProductDummyFactory : IDummyFactory
    {
        public bool CanCreate(Type type) => type == typeof(CoffeeProduct);

        public object? Create(Type type) => CoffeeProductData.GetCoffeeProduct();

        public Priority Priority => Priority.Default;
    }

    public static class CoffeeProductData
    {
        public static CoffeeProduct GetCoffeeProduct()
        {
            var faker = new Faker<CoffeeProduct>()
                .CustomInstantiator(x => new CoffeeProduct
                {
                    Name = x.Commerce.ProductName(),
                    Price = x.Finance.Amount(),
                    CountryOfOrigin = x.Address.Country(),
                    Description = x.Random.String2(MinDescriptionLen, MaxDescriptionLen),
                    IsInStock = true,
                    RoastLevel = x.Random.Enum<RoastLevelType>(),
                    CaffeineContent = x.Random.Enum<CaffeineContentType>(),
                    BeanType = x.Random.Enum<BeanType>(),
                    GrindType = x.Random.Enum<GrindType>(),
                });

            return faker;
        }
    }
}