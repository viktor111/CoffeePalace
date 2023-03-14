using CoffeePalace.Models;
using CoffeePalace.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CoffeePalace.Models.Constants.UserConstants;

namespace CoffeePalace.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).HasMaxLength(MaxFirstNameLen).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(MaxLastnameLen).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(MaxAddressLen).IsRequired();
        builder.Property(x => x.Country).HasMaxLength(MaxCountryLen).IsRequired();
        builder.Property(x => x.City).HasMaxLength(MaxCityLen).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(MaxPhoneNumberLen).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(MaxEmailLen).IsRequired();
    }
}