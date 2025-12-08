using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCampoActivoConsecutivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Consecutivo",
                type: "INTEGER",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Consecutivo");
        }
    }
}
