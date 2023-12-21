using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWebSystem.Migrations
{
    /// <inheritdoc />
    public partial class AnadiendoCIalCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ci",
                table: "Clientes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ci",
                table: "Clientes");
        }
    }
}
