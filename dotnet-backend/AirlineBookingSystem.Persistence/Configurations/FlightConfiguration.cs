using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("flights");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(f => f.FlightNumber).HasColumnName("flight_number").IsRequired();
        builder.Property(f => f.DepartureTime).HasColumnName("departure_time").IsRequired();
        builder.Property(f => f.ArrivalTime).HasColumnName("arrival_time");
        builder.Property(f => f.AirplaneId).HasColumnName("airplane_id").IsRequired();
        builder.Property(f => f.ArrivalGateId).HasColumnName("arrival_gate_id");
        builder.Property(f => f.DepartureGateId).HasColumnName("departure_gate_id").IsRequired();
        builder.Property(f => f.FlightStatusId).HasColumnName("flight_status_id").IsRequired();
        builder.Property(f => f.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(f => f.UpdatedAt).HasColumnName("updated_at");
        builder.Property(f => f.DeletedAt).HasColumnName("deleted_at");
        builder.HasIndex(f => new { f.FlightNumber, f.DepartureTime }).IsUnique();
        builder.HasIndex(f => f.AirplaneId);
        builder.HasIndex(f => f.DepartureGateId);
        builder.HasIndex(f => f.ArrivalGateId);
        builder.HasIndex(f => f.FlightStatusId);
        builder.HasOne(f => f.Airplane)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AirplaneId);
        builder.HasOne(f => f.ArrivalGate)
            .WithMany(g => g.ArrivalFlights)
            .HasForeignKey(f => f.ArrivalGateId);
        builder.HasOne(f => f.DepartureGate)
            .WithMany(g => g.DepartureFlights)
            .HasForeignKey(f => f.DepartureGateId);
        builder.HasOne(f => f.FlightStatus)
            .WithMany(fs => fs.Flights)
            .HasForeignKey(f => f.FlightStatusId);
    }
}
