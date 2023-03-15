using CoffeePalace.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePalace.Services;

public static class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
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
        
        return serviceCollection;
    }
}