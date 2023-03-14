using CoffeePalace.Models.Common;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public interface IReviewFactory : IFactory<Review>
{
    public IReviewFactory SetText(string text);

    public IReviewFactory SetUserId(string userId);

    public IReviewFactory SetRatingType(RatingType ratingType);
}