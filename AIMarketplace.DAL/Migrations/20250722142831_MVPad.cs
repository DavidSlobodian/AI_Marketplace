using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIMarketplace.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MVPad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Ads",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Ads",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Ads",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Ads",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ads");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Ads");
        }
    }
}
