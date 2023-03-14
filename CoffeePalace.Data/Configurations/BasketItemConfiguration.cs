using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePalace.Data.Configurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder
             .HasOne(i => i.CoffeeProduct)
             .WithMany(p => p.BasketItems)
             .HasForeignKey(i => i.CoffeeProductId);
         
         builder
             .HasOne(i => i.Basket)
             .WithMany(p => p.BasketItems)
             .HasForeignKey(i => i.BasketId);
    }
}