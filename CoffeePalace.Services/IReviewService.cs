using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

namespace CoffeePalace.Services;

public interface IReviewService
{
    public Task<Result<Review>> Save(Review review);

    public Task<Result<Review>> Update(string id, Review review);
    
    public Task<Result> Delete(string id);
    
    public Task<Result<IEnumerable<Review>>> All();

    public Task<Result<Review>> GetById(string id);
}