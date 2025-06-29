using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("people");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(p => p.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(p => p.MidName).HasColumnName("mid_name");
        builder.Property(p => p.LastName).HasColumnName("last_name").IsRequired();
        builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth").IsRequired();
        builder.Property(p => p.PhoneNumber).HasColumnName("phone_number");
        builder.Property(p => p.Email).HasColumnName("email");
        builder.Property(p => p.ImagePath).HasColumnName("image_path");
        builder.Property(p => p.GenderId).HasColumnName("gender_id").IsRequired();
        builder.Property(p => p.AddressId).HasColumnName("address_id").IsRequired();
        builder.HasIndex(p => p.AddressId);
        builder.HasIndex(p => p.LastName);
        builder.HasOne(p => p.Gender)
            .WithMany(g => g.People)
            .HasForeignKey(p => p.GenderId);
        builder.HasOne(p => p.Address)
            .WithMany(a => a.People)
            .HasForeignKey(p => p.AddressId);
    }
}
