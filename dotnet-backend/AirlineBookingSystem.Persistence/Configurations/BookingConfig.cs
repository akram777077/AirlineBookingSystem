using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class BookingConfig : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.SeatNumber)
            .HasColumnName("seat_number")
            .HasColumnType("varchar")
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(b => b.FlightId)
            .HasColumnName("flight_id")
            .HasColumnType("integer");
        
        builder.HasOne(b => b.Flight)
            .WithMany()
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(b => b.PassengerId)
            .HasColumnName("passenger_id")
            .HasColumnType("integer");
        
        builder.HasOne(b => b.Passenger)
            .WithMany()
            .HasForeignKey(a => a.PassengerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(b => b.BookingStatusId)
            .HasColumnName("booking_status_id")
            .HasColumnType("integer");
        
        builder.HasOne(b => b.BookingStatus)
            .WithMany()
            .HasForeignKey(b => b.BookingStatusId)
            .OnDelete(DeleteBehavior.SetNull);
        

        builder.HasIndex(b => b.SeatNumber)
            .HasDatabaseName("ix_bookings_seat_number")
            .IsUnique();
    }
}