using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface ICoffeeProductService
{
    public Task<Result<CoffeeProduct>> Save(CoffeeProduct coffeeProduct);
    
    public Task<Result<CoffeeProduct>> Update(string id, CoffeeProduct coffeeProduct);

    public Task<Result> Delete(string id);

    public Task<Result<IEnumerable<CoffeeProduct>>> All();

    public Task<Result<CoffeeProduct>> GetById(string id);
}