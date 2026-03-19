using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuantityMeasurements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThisValue = table.Column<double>(type: "float", nullable: false),
                    ThisUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThisMeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThatValue = table.Column<double>(type: "float", nullable: false),
                    ThatUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThatMeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ResultString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultValue = table.Column<double>(type: "float", nullable: false),
                    ResultUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultMeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsError = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantityMeasurements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_CreatedAt",
                table: "QuantityMeasurements",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_IsError",
                table: "QuantityMeasurements",
                column: "IsError");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_Operation",
                table: "QuantityMeasurements",
                column: "Operation");

            migrationBuilder.CreateIndex(
                name: "IX_QuantityMeasurements_ThisMeasurementType",
                table: "QuantityMeasurements",
                column: "ThisMeasurementType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuantityMeasurements");
        }
    }
}