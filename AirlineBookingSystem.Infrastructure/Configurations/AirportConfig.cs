using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Infrastructure.Configurations;

public class AirportConfig : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("airports");

        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.AirportCode)
            .HasColumnName("airport_code")
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(a => a.CountryId)
            .HasColumnName("country_id")
            .HasColumnType("integer");
        
        builder.HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(a => a.CityId)
            .HasColumnName("city_id")
            .HasColumnType("integer");
        
        builder.HasOne(a => a.City)
            .WithOne()
            .HasForeignKey<Airport>(a => a.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(a => a.AirportCode)
            .HasDatabaseName("ix_airports_airport_code")
            .IsUnique();
    }
}