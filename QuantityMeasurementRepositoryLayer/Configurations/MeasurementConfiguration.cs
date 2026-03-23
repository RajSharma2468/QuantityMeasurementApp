using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Configurations;

public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
{
    public void Configure(EntityTypeBuilder<Measurement> builder)
    {
        builder.ToTable("Measurements");
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.FromUnit)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(m => m.ToUnit)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(m => m.InputValue)
            .HasColumnType("float");
            
        builder.Property(m => m.OutputValue)
            .HasColumnType("float");
        
        builder.HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}