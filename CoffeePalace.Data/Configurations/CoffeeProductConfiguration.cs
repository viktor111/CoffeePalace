using CoffeePalace.Models;
using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CoffeePalace.Models.Constants.CoffeeProductConstants;

namespace CoffeePalace.Data.Configurations;

public class CoffeeProductConfiguration : IEntityTypeConfiguration<CoffeeProduct>
{
    public void Configure(EntityTypeBuilder<CoffeeProduct> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).HasMaxLength(NameMaxLen).IsRequired();
        builder.Property(x => x.Price).HasMaxLength(MaxPrice).IsRequired();
        builder.Property(x => x.CountryOfOrigin).HasMaxLength(MaxCountryOfOriginLen).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(MaxDescriptionLen).IsRequired();
    }
}