using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Infrastructure.Configurations;

public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");

        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Street)
            .HasColumnName("street")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(a => a.ZipCode)
            .HasColumnName("zip_code")
            .HasColumnType("varchar")
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(a => a.CountryId)
            .HasColumnName("country_id")
            .HasColumnType("integer");
        
        builder.HasOne(a => a.Country)
            .WithOne()
            .HasForeignKey<Address>(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(a => a.CityId)
            .HasColumnName("city_id")
            .HasColumnType("integer");
        
        builder.HasOne(a => a.City)
            .WithOne()
            .HasForeignKey<Address>(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(a => a.ZipCode)
            .HasDatabaseName("ix_addresses_zip_code")
            .IsUnique();
    }
}