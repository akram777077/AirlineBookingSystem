using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class FlightStatusConfiguration : IEntityTypeConfiguration<FlightStatus>
{
    public void Configure(EntityTypeBuilder<FlightStatus> builder)
    {
        builder.ToTable("flight_status");
        builder.HasKey(fs => fs.Id);
        builder.Property(fs => fs.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(fs => fs.StatusName).HasColumnName("status_name").IsRequired();
        builder.HasIndex(fs => fs.StatusName).IsUnique();
    }
}
