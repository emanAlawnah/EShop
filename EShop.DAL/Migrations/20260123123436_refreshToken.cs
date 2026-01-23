using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class refreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "USers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "USers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "USers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "USers");
        }
    }
}
