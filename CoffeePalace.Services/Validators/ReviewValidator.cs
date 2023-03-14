using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

using static CoffeePalace.Models.Constants.ReviewConstants;

namespace CoffeePalace.Services.Validators;

public class ReviewValidator
{
    public Result Validate(Review review)
    {
        try
        {
            this.ValidateText(review.Text);
            
            return Result.Success;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private void ValidateText(string text)
    {
        var nameProp = nameof(Review.Text);
        
        if (string.IsNullOrWhiteSpace(text))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (text.Length < MinTextLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinTextLen));
        if (text.Length > MaxTextLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxTextLen));
    }
}