using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePalace.Data;

public static class DataConfiguration
{
    public static IServiceCollection ConfigureData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDatabase(configuration);
        
        return serviceCollection;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(opt =>
        {
            var major = configuration.GetSection("MySqlVersion").GetValue<int>("major");
            var minor = configuration.GetSection("MySqlVersion").GetValue<int>("minor");
            var build = configuration.GetSection("MySqlVersion").GetValue<int>("build");
            
            var version = new Version(major, minor, build);
            
            opt.UseMySql(
                configuration.GetConnectionString("DefaultConnectionString"),
                new MySqlServerVersion(version), 
                x => x.MigrationsAssembly("CoffeePalace.Web"));
        });
        
        return serviceCollection;
    }
}