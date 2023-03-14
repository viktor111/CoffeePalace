using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using CoffeePalace.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class CoffeeProductService : ICoffeeProductService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<CoffeeProductService> logger;
    
    public CoffeeProductService(ApplicationDbContext dbContext, ILogger<CoffeeProductService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Result<CoffeeProduct>> Save(CoffeeProduct coffeeProduct)
    {
        try
        {
            var validation = CoffeeProductValidator.Validate(coffeeProduct);
            if (!validation.Succeeded) return validation.Errors.First();
            
            var result = await this.dbContext.CoffeeProducts.AddAsync(coffeeProduct);
        
            await this.dbContext.SaveChangesAsync();
        
            return result.Entity;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Save(nameof(CoffeeProduct));
        }
    }

    public async Task<Result<CoffeeProduct>> Update(string id, CoffeeProduct coffeeProduct)
    {
        try
        {
            var validation = CoffeeProductValidator.Validate(coffeeProduct);
            if (!validation.Succeeded) return validation.Errors.First();
            
            var old = await dbContext.CoffeeProducts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (old is null) return ErrorMessageBuilder.NotFound(nameof(CoffeeProduct));;

            old.Description = coffeeProduct.Description;
            old.Name = coffeeProduct.Name;
            old.Price = coffeeProduct.Price;
            old.BeanType = coffeeProduct.BeanType;
            old.CaffeineContent = coffeeProduct.CaffeineContent;
            old.GrindType = coffeeProduct.GrindType;
            old.RoastLevel = coffeeProduct.RoastLevel;
            old.CountryOfOrigin = coffeeProduct.CountryOfOrigin;
            old.IsInStock = coffeeProduct.IsInStock;
        
            await this.dbContext.SaveChangesAsync();

            return old;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Update(nameof(CoffeeProduct));
        }
    }

    public async Task<Result> Delete(string id)
    {
        try
        {
            var product = await this.dbContext.CoffeeProducts
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return ErrorMessageBuilder.NotFound(nameof(CoffeeProduct));
        
            this.dbContext.Remove(product);
            
            await this.dbContext.SaveChangesAsync();
            
            return Result.Success;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Delete(nameof(CoffeeProduct));
        }
    }

    public async Task<Result<IEnumerable<CoffeeProduct>>> All()
    {
        try
        {
            var products = await this.dbContext.CoffeeProducts.ToArrayAsync();
            return products;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.All(nameof(CoffeeProduct));
        }
    }
}