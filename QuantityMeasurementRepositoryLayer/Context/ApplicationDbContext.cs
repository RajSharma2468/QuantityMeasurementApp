using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<QuantityMeasurement> QuantityMeasurements { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<QuantityMeasurement>(entity =>
        {
            // Primary Key
            entity.HasKey(e => e.Id);
            
            // Indexes for performance
            entity.HasIndex(e => e.Operation);
            entity.HasIndex(e => e.ThisMeasurementType);
            entity.HasIndex(e => e.CreatedAt);
            entity.HasIndex(e => e.IsError);
            
            // Composite indexes for common queries
            entity.HasIndex(e => new { e.Operation, e.IsError })
                .HasDatabaseName("IX_QuantityMeasurements_Operation_IsError");
                
            entity.HasIndex(e => new { e.CreatedAt, e.Operation })
                .HasDatabaseName("IX_QuantityMeasurements_CreatedAt_Operation");
            
            // Soft delete index (if you added it)
            entity.HasIndex(e => e.IsDeleted)
                .HasDatabaseName("IX_QuantityMeasurements_IsDeleted");
            
            // Property configurations
            entity.Property(e => e.ThisUnit)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.ThisMeasurementType)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.ThatUnit)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.ThatMeasurementType)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.Operation)
                .IsRequired()
                .HasMaxLength(20);
                
            entity.Property(e => e.ResultUnit)
                .HasMaxLength(50);
                
            entity.Property(e => e.ResultMeasurementType)
                .HasMaxLength(50);
            
            // Default values
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
                
            entity.Property(e => e.IsError)
                .HasDefaultValue(false);
                
            // Soft delete default (if you added it)
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);
        });
    }
}