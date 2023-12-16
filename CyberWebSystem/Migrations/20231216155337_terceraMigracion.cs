using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWebSystem.Migrations
{
    /// <inheritdoc />
    public partial class terceraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Flete",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Flete",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Flete");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Flete",
                newName: "MyProperty");
        }
    }
}
