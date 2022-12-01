﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TamagotchiApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFarmName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Farms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Farms");
        }
    }
}
