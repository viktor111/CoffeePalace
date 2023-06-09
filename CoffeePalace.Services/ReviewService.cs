using CoffeePalace.Data;
using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;
using CoffeePalace.Services.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePalace.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext dbContext;
    private readonly ILogger<ReviewService> logger;
    private readonly IValidator<Review> reviewValidator;

    public ReviewService(
        ApplicationDbContext dbContext,
        ILogger<ReviewService> logger,
        IValidator<Review> reviewValidator
        )
    {
        this.dbContext = dbContext;
        this.logger = logger;
        this.reviewValidator = reviewValidator;
    }

    public async Task<Result<Review>> Save(Review review)
    {
        try
        {
            var validation = this.reviewValidator.Validate(review);
            if (!validation.Succeeded) return validation.Errors.First();
            
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
            var validation = this.reviewValidator.Validate(review);
            if (!validation.Succeeded) return validation.Errors.First();
            
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

    public async Task<Result<Review>> GetById(string id)
    {
        try
        {
            var review = await this.dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (review == null)
                return ErrorMessageBuilder.Get(nameof(Review));

            return review;
        }
        catch (Exception e)
        {
            this.logger.LogError("{@e}", e);
            return ErrorMessageBuilder.Get(nameof(Review));
        }
    }
}