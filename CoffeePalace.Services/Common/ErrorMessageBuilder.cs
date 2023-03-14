using Microsoft.AspNetCore.Http.HttpResults;

namespace CoffeePalace.Services.Common;

public static class ErrorMessageBuilder
{
    public static string NotFound(string name)
    {
        return $"{name} is not found";
    }

    public static string NotFound(string name, string message)
    {
        return $"{name} is not found: {message}";
    }
    
    public static string EmptyOrNullCollection(string name)
    {
        return $"{name} is empty or null";
    }
    
    public static string EmptyOrNullCollection(string name, string message)
    {
        return $"{name} is empty or null: {message}";
    }
    
    public static string Update(string name)
    {
        return $"{name} error during update";
    }
    
    public static string Update(string name, string message)
    {
        return $"{name} error during update: {message}";
    }
    
    public static string Delete(string name)
    {
        return $"{name} error during delete";
    }
    
    public static string Delete(string name, string message)
    {
        return $"{name} error during delete: {message}";
    }
    
    public static string Save(string name)
    {
        return $"{name} error during saving";
    }
    
    public static string Save(string name, string message)
    {
        return $"{name} error during saving: {message}";
    }
    
    public static string All(string name)
    {
        return $"{name} error during getting results";
    }
    
    public static string All(string name, string message)
    {
        return $"{name} error during getting results: {message}";
    }

    public static string NullWhiteSpaceOrEmpty(string name)
    {
        return $"{name} cannot be null, whitespace or empty";
    }
    
    public static string NullWhiteSpaceOrEmpty(string name, string message)
    {
        return $"{name} cannot be null, whitespace or empty: {message}";
    }
    
    public static string MinLen(string name, int min)
    {
        return $"{name} does not meet requirement of min len {min}";
    }
    
    public static string MinLen(string name, string message, int min)
    {
        return $"{name} does not meet requirement of min len {min}: {message}";
    }
    
    public static string MaxLen(string name, int min)
    {
        return $"{name} does not meet requirement of max len {min}";
    }
    
    public static string MaxLen(string name, string message, int min)
    {
        return $"{name} does not meet requirement of max len {min}: {message}";
    }
}