using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class UserAirportConfiguration : IEntityTypeConfiguration<UserAirport>
    {
        public void Configure(EntityTypeBuilder<UserAirport> builder)
        {
            builder.ToTable("UserAirports");
            builder.HasKey(ua => new { ua.UserId, ua.AirportId });
            builder.Property(ua => ua.UserId).IsRequired();
            builder.Property(ua => ua.AirportId).IsRequired();

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAirports)
                .HasForeignKey(ua => ua.UserId);

            builder.HasOne(ua => ua.Airport)
                .WithMany(a => a.UserAirports)
                .HasForeignKey(ua => ua.AirportId);

            builder.HasIndex(ua => new { ua.UserId, ua.AirportId }).IsUnique();
        }
    }
}
