using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class GateConfiguration : IEntityTypeConfiguration<Gate>
{
    public void Configure(EntityTypeBuilder<Gate> builder)
    {
        builder.ToTable("gates");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(g => g.GateNumber).HasColumnName("gate_number").IsRequired();
        builder.Property(g => g.TerminalId).HasColumnName("terminal_id").IsRequired();
        builder.HasIndex(g => new { g.TerminalId, g.GateNumber }).IsUnique();
        builder.HasOne(g => g.Terminal)
            .WithMany(t => t.Gates)
            .HasForeignKey(g => g.TerminalId);
    }
}
