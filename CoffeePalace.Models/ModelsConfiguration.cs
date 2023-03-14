using CoffeePalace.Models.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePalace.Models;

public static class ModelsConfiguration
{
    public static IServiceCollection ConfigureModels(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddFactories();
        
        return serviceCollection;
    }

    private static IServiceCollection AddFactories(this IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(scan => scan
            .FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo(typeof(IFactory<>)))
            .AsMatchingInterface()
            .WithTransientLifetime()
        );

        return serviceCollection;
    }
}