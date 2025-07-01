using AirlineBookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlineBookingSystem.Persistence.Configurations
{
    public class ClassTypeConfiguration : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> builder)
        {
            builder.ToTable("ClassTypes");
            builder.HasKey(ct => ct.Id);
            builder.Property(ct => ct.Id).ValueGeneratedOnAdd();
            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion<string>();
            builder.Property(ct => ct.CreatedAt).IsRequired();
            builder.Property(ct => ct.UpdatedAt).IsRequired();
            builder.Property(ct => ct.DeletedAt);
            builder.Property(ct => ct.IsDeleted).HasDefaultValue(false);

            builder.HasMany(ct => ct.FlightClasses)
                .WithOne(fc => fc.ClassType)
                .HasForeignKey(fc => fc.ClassTypeId);

            builder.HasQueryFilter(ct => !ct.IsDeleted);
        }
    }
}
