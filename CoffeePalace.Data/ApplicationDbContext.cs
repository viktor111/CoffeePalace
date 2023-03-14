using System.Reflection;
using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeePalace.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<CoffeeProduct> CoffeeProducts { get; set; }
    
    public DbSet<ImageData> ImageDatas { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<Basket> Baskets { get; set; }
    
    public DbSet<BasketItem> BasketItems { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}