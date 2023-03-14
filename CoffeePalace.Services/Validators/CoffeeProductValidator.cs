using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using FluentValidation;

using static CoffeePalace.Models.Constants.CoffeeProductConstants;

namespace CoffeePalace.Services.Validators;

public class CoffeeProductValidator
{
    public Result Validate(CoffeeProduct coffeeProduct)
    {
        try
        {
            this.ValidateName(coffeeProduct.Name);
            this.ValidateDescription(coffeeProduct.Description);
            this.ValidateCountryOfOrigin(coffeeProduct.CountryOfOrigin);
            this.ValidatePrice(coffeeProduct.Price);
        
            return Result.Success;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private void ValidateName(string name)
    {
        var nameProp = nameof(CoffeeProduct.Name);
        
        if (string.IsNullOrWhiteSpace(name)) 
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameof(nameProp)));
        if (name.Length < NameMinLen) 
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, NameMinLen));
        if (name.Length > NameMaxLen) 
            throw new Exception(ErrorMessageBuilder.MaxLen(nameProp, NameMaxLen));
    }

    private void ValidateDescription(string description)
    {
        var nameProp = nameof(CoffeeProduct.Description);
        
        if (string.IsNullOrWhiteSpace(description)) 
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameof(nameProp)));
        if (description.Length < NameMinLen) 
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinDescriptionLen));
        if (description.Length > NameMaxLen) 
            throw new Exception(ErrorMessageBuilder.MaxLen(nameProp, MaxDescriptionLen));
    }
    
    private void ValidateCountryOfOrigin(string countryOfOrigin)
    {
        var nameProp = nameof(CoffeeProduct.CountryOfOrigin);
        
        if (string.IsNullOrWhiteSpace(countryOfOrigin)) 
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameof(nameProp)));
        if (countryOfOrigin.Length < NameMinLen) 
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinCountryOfOriginLen));
        if (countryOfOrigin.Length > NameMaxLen) 
            throw new Exception(ErrorMessageBuilder.MaxLen(nameProp, MaxCountryOfOriginLen));
    }

    private void ValidatePrice(decimal price)
    {
        var nameProp = nameof(CoffeeProduct.Price);

        if (price < MinPrice) 
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, NameMinLen));
        if (price > MinPrice) 
            throw new Exception(ErrorMessageBuilder.MaxLen(nameProp, NameMaxLen));
    }
}