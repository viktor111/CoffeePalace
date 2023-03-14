using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ReviewService> logger;

    public ReviewService(ApplicationDbContext dbContext, ILogger<ReviewService> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<Result<Review>> Save(Review review)
    {
        try
        {
            var result = await this.dbContext.Reviews.AddAsync(review);
            
            await this.dbContext.SaveChangesAsync();

            return result.Entity;
        }
        catch (Exception e)
        {
            this.logger.LogInformation("{@e}", e);
            return ErrorMessageBuilder.Save(nameof(Review));
        }
    }

    public async Task<Result<Review>> Update(string id, Review review)
    {
        try
        {
            var old = await this.dbContext.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);

            if (old is null) return ErrorMessageBuilder.NotFound(nameof(Review));

            old.Rating = review.Rating;
            old.Text = review.Text;
            old.CoffeeProductId = review.CoffeeProductId;
            old.UserId = review.UserId;

            await this.dbContext.SaveChangesAsync();

            return old;
        }
        catch (Exception e)
        {
            this.logger.LogInformation("{@e}", e);
            return ErrorMessageBuilder.Update(nameof(Review));
        }
    }

    public async Task<Result> Delete(string id)
    {
        try
        {
            var review = await this.dbContext.Reviews
                .FirstOrDefaultAsync(x => x.Id == id);

            if (review is null) return ErrorMessageBuilder.Delete(nameof(Review));

            this.dbContext.Reviews.Remove(review);

            await this.dbContext.SaveChangesAsync();
            
            return Result.Success;
        }
        catch (Exception e)
        {
            this.logger.LogInformation("{@e}", e);
            return ErrorMessageBuilder.Delete(nameof(Review));
        }
    }

    public async Task<Result<IEnumerable<Review>>> All()
    {
        try
        {
            var reviews = await this.dbContext.Reviews.ToArrayAsync();

            return reviews;
        }
        catch (Exception e)
        {
            this.logger.LogInformation("{@e}", e);
            return ErrorMessageBuilder.All(nameof(Review));
        }
    }
}