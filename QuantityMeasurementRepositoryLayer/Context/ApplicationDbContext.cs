using Microsoft.EntityFrameworkCore;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementRepositoryLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
            });

            // RefreshToken configuration
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Token).IsRequired();
                entity.HasOne(rt => rt.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(rt => rt.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Measurement configuration
            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Type).IsRequired().HasMaxLength(50);
                entity.Property(m => m.Unit).IsRequired().HasMaxLength(20);
                entity.Property(m => m.Value).HasColumnType("decimal(18,4)");
                entity.Property(m => m.Notes).HasMaxLength(500);
                entity.Property(m => m.Date).IsRequired();
                entity.HasOne(m => m.User)
                    .WithMany()
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}