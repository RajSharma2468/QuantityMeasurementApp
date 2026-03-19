using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCompositeIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add composite index for better query performance
            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_Operation_IsError",
                table: "QuantityMeasurements",
                columns: new[] { "Operation", "IsError" });
                
            // Add index for date range queries
            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_CreatedAt_Operation",
                table: "QuantityMeasurements",
                columns: new[] { "CreatedAt", "Operation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_QuantityMeasurements_Operation_IsError",
                table: "QuantityMeasurements");
                
            migrationBuilder.DropIndex(
                name: "IX_QuantityMeasurements_CreatedAt_Operation",
                table: "QuantityMeasurements");
        }
    }
}