using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public class CoffeeProductFactory : ICoffeeProductFactory
{
    private string name = default!;
    private decimal price = default!;
    private string countryOfOrigin = default!;
    private string description = default!;
    private bool isInStock = default!;
    private RoastLevelType roastLevel = default!;
    private CaffeineContentType caffeineContent = default!;
    private BeanType beanType = default!;
    private GrindType grindType = default!;

    private bool isNameSet = false;
    private bool isPriceSet = false;
    private bool isCountryOfOriginSet = false;
    private bool isDescriptionSet = false;
    private bool isIsInstockSet = false;
    private bool isRoastLevelSet = false;
    private bool isCaffeineContent = false;
    private bool isBeanTypeSet = false;
    private bool isGrindtypeSet = false;
    
    public ICoffeeProductFactory SetName(string name)
    {
        this.name = name;
        this.isNameSet = true;

        return this;
    }

    public ICoffeeProductFactory SetPrice(decimal price)
    {
        this.price = price;
        this.isPriceSet = true;

        return this;
    }

    public ICoffeeProductFactory SetCountryOfOrigin(string countryOfOrigin)
    {
        this.countryOfOrigin = countryOfOrigin;
        this.isCountryOfOriginSet = true;

        return this;
    }

    public ICoffeeProductFactory SetDescription(string description)
    {
        this.description = description;
        this.isDescriptionSet = true;

        return this;
    }

    public ICoffeeProductFactory SetIsInStock(bool isInStock)
    {
        this.isInStock = isInStock;
        this.isIsInstockSet = true;

        return this;
    }

    public ICoffeeProductFactory SetRoastLevel(RoastLevelType roastLevel)
    {
        this.roastLevel = roastLevel;
        this.isRoastLevelSet = true;

        return this;
    }

    public ICoffeeProductFactory SetCaffeineContent(CaffeineContentType caffeineContent)
    {
        this.caffeineContent = caffeineContent;
        this.isCaffeineContent = true;

        return this;
    }

    public ICoffeeProductFactory SetBeanType(BeanType beanType)
    {
        this.beanType = beanType;
        this.isBeanTypeSet = true;

        return this;
    }

    public ICoffeeProductFactory SetGrindType(GrindType grindType)
    {
        this.grindType = grindType;
        this.isGrindtypeSet = false;

        return this;
    }
    
    public CoffeeProduct Create()
    {
        if (!this.isNameSet) throw new Exception("Name must be set");
        if (!this.isPriceSet) throw new Exception("Price must be set");
        if (!this.isCountryOfOriginSet) throw new Exception("CountryOfOrigin must be set");
        if (!this.isDescriptionSet) throw new Exception("Description must be set");
        if (!this.isIsInstockSet) throw new Exception("IsInStock must be set");
        if (!this.isRoastLevelSet) throw new Exception("RoastLevel must be set");
        if (!this.isCaffeineContent) throw new Exception("CaffeineContent must be set");
        if (!this.isBeanTypeSet) throw new Exception("BeanType must be set");
        if (!this.isGrindtypeSet) throw new Exception("GrindType must be set");

        return new CoffeeProduct
        {
            Name = this.name,
            Price = this.price,
            CountryOfOrigin = this.countryOfOrigin,
            Description = this.description,
            IsInStock = this.isInStock,
            RoastLevel = this.roastLevel,
            CaffeineContent = this.caffeineContent,
            BeanType = this.beanType,
            GrindType = this.grindType
        };
    }
}