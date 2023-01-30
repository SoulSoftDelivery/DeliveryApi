using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApi.Migrations
{
    public partial class AddTableMesas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mesas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesas", x => x.id);
                    table.ForeignKey(
                        name: "FK_mesas_empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mesas_EmpresaId",
                table: "mesas",
                column: "EmpresaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mesas");
        }
    }
}
