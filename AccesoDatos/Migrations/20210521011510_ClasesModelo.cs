using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccesoDatos.Migrations
{
    public partial class ClasesModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rut = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NombreFantasia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    VtoCedula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PathCedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaLibreta = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VtoLibreta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PathLibreta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VtoCarneSalud = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PathCarneSalud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VtoCMAlimentos = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PathCMAlimentos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltaBps = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PathAltaBps = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioRut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FuncionarioId = table.Column<int>(type: "int", nullable: true),
                    EntradaPlanificada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalidaPlanificada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntradaEfectiva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalidaEfectiva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoAutorizacion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingresos_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingresos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_ProveedorId",
                table: "Funcionarios",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_FuncionarioId",
                table: "Ingresos",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_ProveedorId",
                table: "Ingresos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ProveedorId",
                table: "Usuarios",
                column: "ProveedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Proveedores");
        }
    }
}
