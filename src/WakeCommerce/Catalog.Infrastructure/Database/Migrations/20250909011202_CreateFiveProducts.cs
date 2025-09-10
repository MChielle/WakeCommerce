using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateFiveProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "products",
                columns: new[] { "id", "created_at", "name", "price", "quantity", "updated_at" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 1", 1.50m, 10m, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 2", 1.50m, 3.333m, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 3", 1.99m, 4.10m, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 4", 1.50m, 7.10m, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2021, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Produto 5", 1.50m, 3.33m, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "products",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "products",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "products",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "products",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                schema: "public",
                table: "products",
                keyColumn: "id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));
        }
    }
}