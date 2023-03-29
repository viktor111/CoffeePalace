using CoffeePalace.Models.Entities;
using CoffeePalace.Services.Auth;
using CoffeePalace.Services.Common;
using CoffeePalace.Services.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePalace.Services;

public static class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddValidators();
        serviceCollection.AddServices();
        
        return serviceCollection;
    }

    private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICoffeeProductService, CoffeeProductService>();
        serviceCollection.AddScoped<IImageDataService, ImageDataService>();
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        serviceCollection.AddScoped<ITokenStorageService, TokenStorageService>();
        serviceCollection.AddScoped<IImageProcessingService, ImageProcessingService>();
        serviceCollection.AddSingleton<ICountriesService, CountriesService>();
        
        return serviceCollection;
    }

    private static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IValidator<CoffeeProduct>, CoffeeProductValidator>();
        serviceCollection.AddSingleton<IValidator<ImageData>, ImageDataValidator>();
        serviceCollection.AddSingleton<IValidator<Review>, ReviewValidator>();
        serviceCollection.AddSingleton<IValidator<User>, UserValidator>();

        return serviceCollection;
    }
}