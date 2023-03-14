using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface IBasketService
{
    public Task<Result<IEnumerable<CoffeeProduct>>> GetAllProducts(string userId);

    public Task<Result<CoffeeProduct>> AddProductToBasket(string userId, string productId);

    public Task<Result<CoffeeProduct>> RemoveProductFromBasket(string userId, string productId);

    public Task<Result> ClearBasket(string userId);
}