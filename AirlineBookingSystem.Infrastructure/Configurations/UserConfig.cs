using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .HasColumnName("username")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar")
                .HasMaxLength(255)
                .IsRequired();
            
            builder.Property(u => u.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.LastLogin)
                .HasColumnName("last_login_at")
                .HasColumnType("timestamp with time zone");

            builder.Property(u => u.PersonId)
                .HasColumnName("person_id")
                .HasColumnType("integer");

            builder.HasOne(u => u.Person)
                .WithMany()
                .HasForeignKey(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.RoleId)
                .HasColumnName("role_id")
                .HasColumnType("integer");

            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Username)
                .HasDatabaseName("ix_users_username")
                .IsUnique();

            builder.HasIndex(u => u.PersonId)
                .HasDatabaseName("ix_users_person_id")
                .IsUnique();

            builder.ToTable("users");
        }
    }
}