using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasurementsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUnit",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "InputValue",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "ToUnit",
                table: "Measurements",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "OutputValue",
                table: "Measurements",
                newName: "Value");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Measurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Measurements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Measurements",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Measurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Measurements");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Measurements",
                newName: "OutputValue");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Measurements",
                newName: "ToUnit");

            migrationBuilder.AddColumn<string>(
                name: "FromUnit",
                table: "Measurements",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "InputValue",
                table: "Measurements",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
