using System.Linq.Expressions;
using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;

namespace CoffeePalace.Models.Factories;

public class ReviewFactory : IReviewFactory
{
    private string text = default!;
    private string userId = default!;
    private RatingType ratingType = default!;

    private bool isTextSet = false;
    private bool isUserIdSet = false;
    private bool isRatingTypeSet = false;
    
    public IReviewFactory SetText(string text)
    {
        this.text = text;
        this.isTextSet = true;

        return this;
    }

    public IReviewFactory SetUserId(string userId)
    {
        this.userId = userId;
        this.isUserIdSet = false;

        return this;
    }

    public IReviewFactory SetRatingType(RatingType ratingType)
    {
        this.ratingType = ratingType;
        this.isRatingTypeSet = false;

        return this;
    }
    
    public Review Create()
    {
        if (!this.isTextSet) throw new Exception("Text is not set");
        if (!this.isUserIdSet) throw new Exception("UserId is not set");
        if (!this.isRatingTypeSet) throw new Exception("RatingType is not set");

        return new Review
        {
            Text = this.text,
            UserId = this.userId,
            Rating = this.ratingType
        };
    }
}