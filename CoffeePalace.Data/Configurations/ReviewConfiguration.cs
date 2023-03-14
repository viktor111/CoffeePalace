using CoffeePalace.Models;
using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CoffeePalace.Models.Constants.ReviewConstants;

namespace CoffeePalace.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text).HasMaxLength(MaxTextLen).IsRequired();

        builder
            .HasOne(x => x.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(x => x.UserId);
        
        builder
            .HasOne(x => x.CoffeeProduct)
            .WithMany(u => u.Reviews)
            .HasForeignKey(x => x.CoffeeProductId);
    }
}