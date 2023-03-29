using CoffeePalace.Models.Entities;
using CoffeePalace.Models.Types;
using CoffeePalace.Services.Common;

using static CoffeePalace.Models.Constants.UserConstants;

namespace CoffeePalace.Services.Validators;

public static class UserValidator
{
    public static Result Validate(User user)
    {
        try
        {
            ValidateFirstName(user.FirstName);
            ValidateLastName(user.LastName);
            ValidateAddress(user.Address);
            ValidateCity(user.City);
            ValidateCountry(user.Country);
            ValidatePhoneNumber(user.PhoneNumber);
            ValidateEmail(user.Email);
            ValidatePassword(user.Password);
            
            return Result.Success;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    private static void ValidateFirstName(string firstName)
    {
        var nameProp = nameof(User.FirstName);

        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (firstName.Length < MinFirstNameLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinFirstNameLen));
        if (firstName.Length > MaxFirstNameLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxFirstNameLen));
    }

    private static void ValidateLastName(string lastName)
    {
        var nameProp = nameof(User.LastName);

        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (lastName.Length < MinLastnameLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinLastnameLen));
        if (lastName.Length > MaxLastnameLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxLastnameLen));
    }

    private static void ValidateAddress(string address)
    {
        var nameProp = nameof(User.Address);

        if (string.IsNullOrWhiteSpace(address))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (address.Length < MinAddressLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinAddressLen));
        if (address.Length > MaxAddressLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxAddressLen));
    }

    private static void ValidateCountry(string country)
    {
        var nameProp = nameof(User.Country);

        if (string.IsNullOrWhiteSpace(country))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (country.Length < MinCountryLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinCountryLen));
        if (country.Length > MaxCountryLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxCountryLen));

        var validCountries = CountriesList.GetCountryNamesToLower();

        if (!validCountries.Contains(country.ToLower()))
            throw new Exception(ErrorMessageBuilder.NotFound(nameProp));
    }

    private static void ValidateCity(string city)
    {
        var nameProp = nameof(User.City);

        if (string.IsNullOrWhiteSpace(city))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (city.Length < MinCityLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinCityLen));
        if (city.Length > MaxCityLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxCityLen));
    }

    private static void ValidatePhoneNumber(string phoneNumber)
    {
        var nameProp = nameof(User.PhoneNumber);

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (phoneNumber.Length < MinPhoneNumberLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinPhoneNumberLen));
        if (phoneNumber.Length > MaxPhoneNumberLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxPhoneNumberLen));
    }

    private static void ValidateEmail(string email)
    {
        var nameProp = nameof(User.Email);

        if (string.IsNullOrWhiteSpace(email))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (email.Length < MinEmailLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinEmailLen));
        if (email.Length > MaxEmailLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxEmailLen));
    }

    private static void ValidatePassword(string password)
    {
        var nameProp = nameof(User.Password);

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception(ErrorMessageBuilder.NullWhiteSpaceOrEmpty(nameProp));
        if (password.Length < MinPasswordLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MinPasswordLen));
        if (password.Length > MaxPasswordLen)
            throw new Exception(ErrorMessageBuilder.MinLen(nameProp, MaxPasswordLen));
    }
}