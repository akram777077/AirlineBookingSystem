using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class TerminalConfiguration : IEntityTypeConfiguration<Terminal>
{
    public void Configure(EntityTypeBuilder<Terminal> builder)
    {
        builder.ToTable("terminals");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(t => t.Name).HasColumnName("name").IsRequired();
        builder.Property(t => t.AirportId).HasColumnName("airport_id").IsRequired();
        builder.HasIndex(t => new { t.AirportId, t.Name }).IsUnique();
        builder.HasOne(t => t.Airport)
            .WithMany(a => a.Terminals)
            .HasForeignKey(t => t.AirportId);
    }
}
