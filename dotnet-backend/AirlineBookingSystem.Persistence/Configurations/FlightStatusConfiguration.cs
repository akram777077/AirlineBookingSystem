using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class FlightStatusConfiguration : IEntityTypeConfiguration<FlightStatus>
{
    public void Configure(EntityTypeBuilder<FlightStatus> builder)
    {
        builder.HasKey(fs => fs.Id);

        builder.Property(fs => fs.StatusName)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(fs => fs.StatusName).IsUnique();
    }
}