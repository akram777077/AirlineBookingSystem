using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(b => b.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(b => b.FlightId).HasColumnName("flight_id").IsRequired();
            builder.Property(b => b.BookingStatusId).HasColumnName("booking_status_id").IsRequired();
            builder.Property(b => b.BookedAt).HasColumnName("booked_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.TicketNumber).HasColumnName("ticket_number");
            builder.Property(b => b.PaymentStatus).HasColumnName("payment_status").IsRequired();
            builder.Property(b => b.SeatId).HasColumnName("seat_id");
            builder.Property(b => b.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.UpdatedAt).HasColumnName("updated_at");
            builder.Property(b => b.DeletedAt).HasColumnName("deleted_at");

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
                .WithOne(s => s.Booking)
                .HasForeignKey<Booking>(b => b.SeatId)
                .IsRequired(false);

            builder.HasIndex(b => b.TicketNumber).IsUnique();
            builder.HasIndex(b => b.SeatId).IsUnique();
            builder.HasIndex(b => new { b.UserId, b.FlightId }).IsUnique();
        }
    }
}
