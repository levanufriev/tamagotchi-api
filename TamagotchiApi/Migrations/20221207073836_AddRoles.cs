using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TamagotchiApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71095483-dcff-47d9-bfb6-499aff57fdd8", "a30ef1f8-f9e1-465a-93b8-08a029b35daa", "user", "USER" },
                    { "d24f727c-9172-47b4-a730-82abbd5487ac", "3dc2f74a-baf5-4e58-b2d8-21167c1bf5f8", "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71095483-dcff-47d9-bfb6-499aff57fdd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d24f727c-9172-47b4-a730-82abbd5487ac");
        }
    }
}
