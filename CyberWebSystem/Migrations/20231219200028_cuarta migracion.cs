using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWebSystem.Migrations
{
    /// <inheritdoc />
    public partial class cuartamigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Flete",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hora",
                table: "Flete",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Flete",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Flete");

            migrationBuilder.DropColumn(
                name: "Hora",
                table: "Flete");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Flete");
        }
    }
}
