using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.ToTable("airports");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(a => a.AirportCode).HasColumnName("airport_code").IsRequired();
        builder.Property(a => a.Name).HasColumnName("name").IsRequired();
        builder.Property(a => a.CityId).HasColumnName("city_id").IsRequired();
        builder.Property(a => a.Timezone).HasColumnName("timezone").IsRequired();
        builder.HasIndex(a => a.AirportCode).IsUnique();
        builder.HasOne(a => a.City)
            .WithMany(c => c.Airports)
            .HasForeignKey(a => a.CityId);
    }
}
