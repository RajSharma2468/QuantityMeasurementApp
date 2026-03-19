using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add soft delete column
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuantityMeasurements",
                type: "bit",
                nullable: false,
                defaultValue: false);
                
            // Add index for soft delete queries
            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_IsDeleted",
                table: "QuantityMeasurements",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuantityMeasurements");
                
            migrationBuilder.DropIndex(
                name: "IX_QuantityMeasurements_IsDeleted",
                table: "QuantityMeasurements");
        }
    }
}