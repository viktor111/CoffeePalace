using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class BasketService : IBasketService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<BasketService> logger;

    public BasketService(ApplicationDbContext dbContext, ILogger<BasketService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }
    
    public async Task<Result<IEnumerable<CoffeeProduct>>> GetAllProducts(string userId)
    {
        try
        {
            var userBasket = await this.dbContext.Baskets
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (userBasket is null) return ErrorMessageBuilder.NotFound(nameof(Basket));

            var basketItems = await this.dbContext.BasketItems
                .Where(x => x.BasketId == userBasket.Id)
                .Include(x => x.CoffeeProduct)
                .Select(x => x.CoffeeProduct)
                .ToArrayAsync();

            return basketItems;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.All(nameof(BasketItem));
        }
    }

    public async Task<Result<CoffeeProduct>> AddProductToBasket(string userId, string productId)
    {
        try
        {
            var userBasket = await this.dbContext.Baskets
                .FirstOrDefaultAsync(x => x.UserId == userId);
        
            if (userBasket is null) return ErrorMessageBuilder.NotFound(nameof(Basket));
        
            var product = await this.dbContext.CoffeeProducts
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product is null) return ErrorMessageBuilder.NotFound(nameof(CoffeeProduct));

            var basketItem = await this.dbContext.BasketItems
                .FirstOrDefaultAsync(x => x.BasketId == userBasket.Id && x.CoffeeProductId == product.Id);

            if (basketItem is null)
            {
                var basketItemToAdd = new BasketItem
                {
                    BasketId = userBasket.Id,
                    Basket = userBasket,
                    CoffeeProductId = product.Id,
                    CoffeeProduct = product,
                    Quantity = 1
                };

                await this.dbContext.BasketItems.AddAsync(basketItemToAdd);

                await this.dbContext.SaveChangesAsync();
            
                return product;
            }

            basketItem.Quantity += 1;
        
            await this.dbContext.SaveChangesAsync();

            return product;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Save(nameof(BasketItem), "Could not add product to basket");
        }
    }

    public async Task<Result<CoffeeProduct>> RemoveProductFromBasket(string userId, string productId)
    {
        try
        {
            var userBasket = await this.dbContext.Baskets
                .FirstOrDefaultAsync(x => x.UserId == userId);
        
            if (userBasket is null) return ErrorMessageBuilder.NotFound(nameof(Basket));
        
            var product = await this.dbContext.CoffeeProducts
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product is null) return ErrorMessageBuilder.NotFound(nameof(CoffeeProduct));
        
            var basketItem = await this.dbContext.BasketItems
                .FirstOrDefaultAsync(x => x.BasketId == userBasket.Id && x.CoffeeProductId == product.Id);

            if (basketItem is null) return ErrorMessageBuilder.Delete(nameof(BasketItem), "item is not in basket");

            if (basketItem.Quantity > 1)
            {
                basketItem.Quantity -= 1;

                await this.dbContext.SaveChangesAsync();
            
                return product;
            }

            this.dbContext.BasketItems.Remove(basketItem);
        
            await this.dbContext.SaveChangesAsync();

            return product;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Delete(nameof(BasketItem));
        }
    }

    public async Task<Result> ClearBasket(string userId)
    {
        try
        {
            var userBasket = await this.dbContext.Baskets
                .FirstOrDefaultAsync(x => x.UserId == userId);
        
            if (userBasket is null) return ErrorMessageBuilder.NotFound(nameof(Basket));

            var basketItems = await this.dbContext.BasketItems
                .Where(x => x.BasketId == userBasket.Id)
                .ToArrayAsync();

            if (!basketItems.Any()) return ErrorMessageBuilder.Delete(nameof(BasketItem), "no items in basket");

            this.dbContext.BasketItems.RemoveRange(basketItems);

            await this.dbContext.SaveChangesAsync();

            return Result.Success;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Delete(nameof(BasketItem));
        }
    }
}