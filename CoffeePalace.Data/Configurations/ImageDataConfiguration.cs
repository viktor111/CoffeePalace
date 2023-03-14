using CoffeePalace.Models;
using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CoffeePalace.Models.Constants.ImageDataConstants;

namespace CoffeePalace.Data.Configurations;

public class ImageDataConfiguration : IEntityTypeConfiguration<ImageData>
{
    public void Configure(EntityTypeBuilder<ImageData> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(MaxNameLen).IsRequired();
    }
}