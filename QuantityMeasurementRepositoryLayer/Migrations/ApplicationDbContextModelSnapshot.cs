using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using QuantityMeasurementRepositoryLayer.Context;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuantityMeasurementModelLayer.Entities.QuantityMeasurement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsError")
                        .HasColumnType("bit");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ResultMeasurementType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ResultString")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResultUnit")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("ResultValue")
                        .HasColumnType("float");

                    b.Property<string>("ThatMeasurementType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ThatUnit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("ThatValue")
                        .HasColumnType("float");

                    b.Property<string>("ThisMeasurementType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ThisUnit")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("ThisValue")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    // If you added the soft delete column
                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("IsError");

                    b.HasIndex("Operation");

                    b.HasIndex("ThisMeasurementType");
                    
                    // If you added the composite indexes
                    b.HasIndex("Operation", "IsError")
                        .HasDatabaseName("IX_QuantityMeasurements_Operation_IsError");
                        
                    b.HasIndex("CreatedAt", "Operation")
                        .HasDatabaseName("IX_QuantityMeasurements_CreatedAt_Operation");
                        
                    // If you added soft delete
                    b.HasIndex("IsDeleted")
                        .HasDatabaseName("IX_QuantityMeasurements_IsDeleted");

                    b.ToTable("QuantityMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}