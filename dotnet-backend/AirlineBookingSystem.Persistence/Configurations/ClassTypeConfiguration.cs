using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class ClassTypeConfiguration : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> builder)
        {
            builder.ToTable("class_types");
            builder.HasKey(ct => ct.Id);
            builder.Property(ct => ct.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(ct => ct.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasConversion<string>();
            
            builder.HasIndex(ct => ct.Name).IsUnique();

            builder.HasMany(ct => ct.FlightClasses)
                .WithOne(fc => fc.ClassType)
                .HasForeignKey(fc => fc.ClassTypeId);
            
        }
    }
}
