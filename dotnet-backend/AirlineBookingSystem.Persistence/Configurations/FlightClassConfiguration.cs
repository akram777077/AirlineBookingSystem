using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class FlightClassConfiguration : IEntityTypeConfiguration<FlightClass>
    {
        public void Configure(EntityTypeBuilder<FlightClass> builder)
        {
            builder.ToTable("FlightClasses");
            builder.HasKey(fc => fc.Id);
            builder.Property(fc => fc.Id).ValueGeneratedOnAdd();
            builder.Property(fc => fc.FlightId).IsRequired();
            builder.Property(fc => fc.ClassTypeId).IsRequired();
            builder.Property(fc => fc.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(fc => fc.CreatedAt).IsRequired();
            builder.Property(fc => fc.UpdatedAt).IsRequired();
            builder.Property(fc => fc.DeletedAt);
            builder.Property(fc => fc.IsDeleted).HasDefaultValue(false);

            builder.HasOne(fc => fc.Flight)
                .WithMany(f => f.FlightClasses)
                .HasForeignKey(fc => fc.FlightId);

            builder.HasOne(fc => fc.ClassType)
                .WithMany(ct => ct.FlightClasses)
                .HasForeignKey(fc => fc.ClassTypeId);

            builder.HasMany(fc => fc.Seats)
                .WithOne(s => s.FlightClass)
                .HasForeignKey(s => s.FlightClassId);

            builder.HasIndex(fc => new { fc.FlightId, fc.ClassTypeId }).IsUnique();

            builder.HasQueryFilter(fc => !fc.IsDeleted);
        }
    }
}
