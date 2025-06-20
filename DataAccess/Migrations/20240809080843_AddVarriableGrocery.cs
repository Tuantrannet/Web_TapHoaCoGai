using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddVarriableGrocery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groceries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groceries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Groceries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Groceries");

            migrationBuilder.InsertData(
                table: "Groceries",
                columns: new[] { "Id", "ExpiryDate", "HighPrice", "ImageUrl", "LowPrice", "Name", "ProductionDate", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 90.0, "", 3.0, "Mỳ tôm 3 miền", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mỳ tôm" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 140.0, "", 26.0, "Thuốc ngựa", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thuốc lá" }
                });
        }
    }
}
