using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class FlightConfig : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("flights");

        builder.HasKey(f => f.Id);
        
        builder.Property(f => f.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(f => f.FlightNumber)
            .HasColumnName("flight_number")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(f => f.DepartureTime)
            .HasColumnName("departure_time")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(f => f.ArrivalTime)
            .HasColumnName("arrival_time")
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);
        
        builder.Property(f => f.FromAirportId)
            .HasColumnName("from_airport_id")
            .HasColumnType("integer");
        
        builder.HasOne(f => f.FromAirport)
            .WithMany()
            .HasForeignKey(f => f.FromAirportId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(f => f.ToAirportId)
            .HasColumnName("to_airport_id")
            .HasColumnType("integer");
        
        builder.HasOne(f => f.ToAirport)
            .WithMany()
            .HasForeignKey(f => f.ToAirportId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(f => new { f.FlightNumber, f.DepartureTime })
            .HasDatabaseName("ix_flights_flight_number_departure_time")
            .IsUnique();
    }
}