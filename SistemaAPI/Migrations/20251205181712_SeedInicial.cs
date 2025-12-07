using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SistemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    AuditoriaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    ConsecutivoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Accion = table.Column<string>(type: "TEXT", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditoria", x => x.AuditoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Consecutivo",
                columns: table => new
                {
                    ConsecutivoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FolioCompleto = table.Column<string>(type: "TEXT", nullable: true),
                    Remitente = table.Column<string>(type: "TEXT", nullable: true),
                    Destinatario = table.Column<string>(type: "TEXT", nullable: true),
                    Fecha = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Asunto = table.Column<string>(type: "TEXT", nullable: true),
                    UsuarioID = table.Column<int>(type: "INTEGER", nullable: false),
                    CanceladoPor = table.Column<int>(type: "INTEGER", nullable: false),
                    MotivoCan = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consecutivo", x => x.ConsecutivoID);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    ReporteID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ConsecutivoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Asunto = table.Column<string>(type: "TEXT", nullable: true),
                    Registro = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.ReporteID);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    RolID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreRol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.RolID);
                });

            migrationBuilder.CreateTable(
                name: "Sistema",
                columns: table => new
                {
                    SistemaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Gerencia = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistema", x => x.SistemaID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumEmpleado = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreUsuario = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    IDRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                });

            migrationBuilder.InsertData(
                table: "Rol",
                columns: new[] { "RolID", "NombreRol" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Usuario" }
                });

            migrationBuilder.InsertData(
                table: "Sistema",
                columns: new[] { "SistemaID", "Gerencia" },
                values: new object[] { 1, "ALS" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "UsuarioID", "IDRol", "NombreUsuario", "NumEmpleado", "Password" },
                values: new object[,]
                {
                    { 1, 1, "Olga Vázquez", 3248, "pass" },
                    { 2, 2, "Usuario Comun", 1001, "userpass" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Consecutivo");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "Sistema");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
