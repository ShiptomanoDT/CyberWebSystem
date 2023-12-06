using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWebSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddEquipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Equipos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Equipos");
        }
    }
}
