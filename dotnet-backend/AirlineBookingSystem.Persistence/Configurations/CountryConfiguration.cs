using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasColumnName("name").IsRequired();
        builder.Property(c => c.Code).HasColumnName("code").IsRequired();
        builder.HasIndex(c => c.Code).IsUnique();
    }
}
