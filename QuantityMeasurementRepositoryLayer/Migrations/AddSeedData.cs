using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert sample data for testing
            migrationBuilder.InsertData(
                table: "QuantityMeasurements",
                columns: new[] { 
                    "ThisValue", "ThisUnit", "ThisMeasurementType", 
                    "ThatValue", "ThatUnit", "ThatMeasurementType",
                    "Operation", "ResultString", "ResultValue", "IsError", "CreatedAt" 
                },
                values: new object[,]
                {
                    {
                        1.0, "Feet", "LengthUnit",
                        12.0, "Inches", "LengthUnit",
                        "compare", "true", 0.0, false, DateTime.UtcNow
                    },
                    {
                        1.0, "Feet", "LengthUnit",
                        0.0, "Inches", "LengthUnit",
                        "convert", null, 12.0, false, DateTime.UtcNow
                    },
                    {
                        1.0, "Feet", "LengthUnit",
                        12.0, "Inches", "LengthUnit",
                        "add", null, 2.0, false, DateTime.UtcNow
                    },
                    {
                        10.0, "Feet", "LengthUnit",
                        2.0, "Feet", "LengthUnit",
                        "divide", null, 5.0, false, DateTime.UtcNow
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove seed data
            migrationBuilder.Sql("DELETE FROM QuantityMeasurements");
        }
    }
}