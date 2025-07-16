using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Code)
            .IsRequired()
            .HasMaxLength(1);

        builder.HasIndex(g => g.Code).IsUnique();
    }
}