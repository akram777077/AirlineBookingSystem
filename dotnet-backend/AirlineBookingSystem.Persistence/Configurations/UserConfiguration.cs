using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(u => u.Username).HasColumnName("username").IsRequired();
        builder.Property(u => u.Password).HasColumnName("password").IsRequired();
        builder.Property(u => u.LastLoginAt).HasColumnName("last_login_at");
        builder.Property(u => u.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        builder.Property(u => u.PersonId).HasColumnName("person_id").IsRequired();
        builder.Property(u => u.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at");
        builder.Property(u => u.DeletedAt).HasColumnName("deleted_at");
        builder.HasIndex(u => u.Username).IsUnique().HasDatabaseName("active_username");
        builder.HasIndex(u => u.RoleId);
        builder.HasOne(u => u.Person)
            .WithOne(p => p.User)
            .HasForeignKey<User>(u => u.PersonId);
        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
    }
}
