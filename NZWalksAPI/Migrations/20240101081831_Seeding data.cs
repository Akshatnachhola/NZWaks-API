using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("70db6a7a-8c48-4480-aa79-87dfa933d8e9"), "Hard" },
                    { new Guid("745aa1b9-d722-477f-8ff0-45ebe92d1a2c"), "Medium" },
                    { new Guid("ac485118-365c-47ab-b689-108dc1a892f5"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("119992a6-5d88-412d-9be2-6dc87a288cd9"), "WGN", "Wellington", null },
                    { new Guid("21dc44fc-281a-4f62-a305-84c81abae3d9"), "AKL", "Auckland", null },
                    { new Guid("a2a81496-0898-4877-9cfd-15aaef5f3d90"), "STL", "SouthLand", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("70db6a7a-8c48-4480-aa79-87dfa933d8e9"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("745aa1b9-d722-477f-8ff0-45ebe92d1a2c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ac485118-365c-47ab-b689-108dc1a892f5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("119992a6-5d88-412d-9be2-6dc87a288cd9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("21dc44fc-281a-4f62-a305-84c81abae3d9"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a2a81496-0898-4877-9cfd-15aaef5f3d90"));
        }
    }
}
