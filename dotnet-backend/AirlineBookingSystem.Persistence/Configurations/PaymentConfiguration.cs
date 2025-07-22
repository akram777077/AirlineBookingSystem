using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(p => p.BookingId).HasColumnName("booking_id").IsRequired();
            builder.Property(p => p.Amount).HasColumnName("amount").IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.PaymentMethod).HasColumnName("method").IsRequired();
            builder.Property(p => p.TransactionId).HasColumnName("transaction_id").IsRequired();
            builder.Property(p => p.PaymentDate).HasColumnName("paid_at").IsRequired();

            builder.HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .IsRequired();
            
            builder.HasIndex(p => p.TransactionId).IsUnique();
        }
    }
}
