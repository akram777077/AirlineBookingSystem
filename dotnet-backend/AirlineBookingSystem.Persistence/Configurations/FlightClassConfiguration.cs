using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class FlightClassConfiguration : IEntityTypeConfiguration<FlightClass>
    {
        public void Configure(EntityTypeBuilder<FlightClass> builder)
        {
            builder.ToTable("flight_classes");
            builder.HasKey(fc => fc.Id);
            builder.Property(fc => fc.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(fc => fc.FlightId).HasColumnName("flight_id").IsRequired();
            builder.Property(fc => fc.ClassTypeId).HasColumnName("class_type_id").IsRequired();
            builder.Property(fc => fc.SeatCapacity).HasColumnName("seat_capacity").IsRequired();
            builder.Property(fc => fc.Price).HasColumnName("price").IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(fc => fc.Flight)
                .WithMany(f => f.FlightClasses)
                .HasForeignKey(fc => fc.FlightId);

            builder.HasOne(fc => fc.ClassType)
                .WithMany(ct => ct.FlightClasses)
                .HasForeignKey(fc => fc.ClassTypeId);

            builder.HasIndex(fc => new { fc.FlightId, fc.ClassTypeId }).IsUnique();
        }
    }
}
