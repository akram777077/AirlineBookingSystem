using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(p => p.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.MidName)
                .HasColumnName("mid_name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(false);
            
            builder.Property(p => p.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.DOB)
                .HasColumnName("date_of_birth")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnName("phone_number")
                .HasColumnType("varchar")
                .HasMaxLength(20);

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(p => p.ImagePath)
                .HasColumnName("image_path")
                .HasColumnType("varchar")
                .HasMaxLength(500);

            builder.Property(p => p.Gender)
                .HasColumnName("gender")
                .HasColumnType("char(1)");

            builder.Property(p => p.AddressId)
                .HasColumnName("address_id")
                .HasColumnType("integer");

            builder.HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(p => p.LastName)
                .HasDatabaseName("ix_people_last_name");

            builder.ToTable("people");
        }
    }
}
