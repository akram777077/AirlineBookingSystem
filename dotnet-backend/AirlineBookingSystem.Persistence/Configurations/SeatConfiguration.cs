using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("Seats");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.FlightClassId).IsRequired();
            builder.Property(s => s.SeatNumber).IsRequired().HasMaxLength(10);
            builder.Property(s => s.IsAvailable).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.UpdatedAt).IsRequired();
            builder.Property(s => s.DeletedAt);
            builder.Property(s => s.IsDeleted).HasDefaultValue(false);

            builder.HasOne(s => s.FlightClass)
                .WithMany(fc => fc.Seats)
                .HasForeignKey(s => s.FlightClassId);

            builder.HasMany(s => s.Bookings)
                .WithOne(b => b.Seat)
                .HasForeignKey(b => b.SeatId);

            builder.HasIndex(s => new { s.FlightClassId, s.SeatNumber }).IsUnique();

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
