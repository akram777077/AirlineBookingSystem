using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
{
    public void Configure(EntityTypeBuilder<BookingStatus> builder)
    {
        builder.ToTable("booking_status");
        builder.HasKey(bs => bs.Id);
        builder.Property(bs => bs.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(bs => bs.BookingStatusName).HasColumnName("booking_status_name").IsRequired();
    }
}
