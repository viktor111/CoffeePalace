using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Common;

using static CoffeePalace.Models.Constants.ImageDataConstants;

namespace CoffeePalace.Services.Validators;

public class ImageDataValidator : IValidator<ImageData>
{
    public Result Validate(ImageData imageData)
    {
        try
        {
            ValidateName(imageData.Name);
            
            return Result.Success;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private void ValidateName(string name)
    {
        var nameProp = nameof(ImageData.Name);
        
        if (string.IsNullOrWhiteSpace(name)) 
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameof(nameProp)));
        if (name.Length < MinNameLen) 
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinNameLen));
        if (name.Length > MaxNameLen) 
            throw new Exception(ErrorMessageBuilder.MaxLen(nameProp, MaxNameLen));
    }
}