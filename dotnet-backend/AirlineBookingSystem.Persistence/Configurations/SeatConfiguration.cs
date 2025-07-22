using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> builder)
        {
            builder.ToTable("seats");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(s => s.ClassTypesId).HasColumnName("class_types_id").IsRequired();
            builder.Property(s => s.SeatNumber).HasColumnName("seat_number").IsRequired();
            builder.Property(s => s.IsReserved).HasColumnName("is_reserved").IsRequired().HasDefaultValue(false);
            builder.Property(s => s.AirplaneId).HasColumnName("airplane_id").IsRequired();

            builder.HasOne(s => s.ClassType)
                .WithMany(ct => ct.Seats)
                .HasForeignKey(s => s.ClassTypesId);

            builder.HasOne(s => s.Airplane)
                .WithMany(a => a.Seats)
                .HasForeignKey(s => s.AirplaneId);

            builder.HasIndex(s => new { s.ClassTypesId, s.SeatNumber }).IsUnique();
        }
    }
}
