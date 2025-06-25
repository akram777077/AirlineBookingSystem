using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class PassengerConfig : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.UserId)
            .HasColumnName("user_id")
            .HasColumnType("integer");

        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Passenger>(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("passengers");
    }
}