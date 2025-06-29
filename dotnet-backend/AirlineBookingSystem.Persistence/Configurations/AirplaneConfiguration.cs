using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
{
    public void Configure(EntityTypeBuilder<Airplane> builder)
    {
        builder.ToTable("airplanes");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(a => a.Model).HasColumnName("model").IsRequired();
        builder.Property(a => a.Manufacturer).HasColumnName("manufacturer").IsRequired();
        builder.Property(a => a.Capacity).HasColumnName("capacity").IsRequired();
        builder.Property(a => a.Code).HasColumnName("code").IsRequired();
        builder.HasIndex(a => a.Code).IsUnique();
    }
}
