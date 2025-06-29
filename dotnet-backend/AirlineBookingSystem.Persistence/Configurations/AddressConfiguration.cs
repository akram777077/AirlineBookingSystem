using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(a => a.Street).HasColumnName("street").IsRequired();
        builder.Property(a => a.ZipCode).HasColumnName("zip_code").IsRequired();
        builder.Property(a => a.CityId).HasColumnName("city_id").IsRequired();
        builder.HasIndex(a => a.ZipCode).IsUnique();
        builder.HasOne(a => a.City)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CityId);
    }
}
