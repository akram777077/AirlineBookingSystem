using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.TicketNumber).IsRequired(false);
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.FlightId).IsRequired();
            builder.Property(b => b.BookingStatusId).IsRequired();
            builder.Property(b => b.SeatId).IsRequired(false);
            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.UpdatedAt);
            builder.Property(b => b.DeletedAt);

            builder.HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            builder.HasOne(b => b.Flight)
                .WithMany(f => f.Bookings)
                .HasForeignKey(b => b.FlightId);

            builder.HasOne(b => b.BookingStatus)
                .WithMany(bs => bs.Bookings)
                .HasForeignKey(b => b.BookingStatusId);

            builder.HasOne(b => b.Seat)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.SeatId)
                .IsRequired(false);

            builder.HasIndex(b => b.TicketNumber).IsUnique();
            builder.HasIndex(b => b.SeatId).IsUnique();
            builder.HasIndex(b => new { b.UserId, b.FlightId }).IsUnique();
        }
    }
}
