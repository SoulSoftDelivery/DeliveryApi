using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApi.Migrations
{
    public partial class addenderecointoempresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_empresas_enderecos_endereco_id",
                table: "empresas");

            migrationBuilder.DropIndex(
                name: "IX_empresas_endereco_id",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "endereco_id",
                table: "empresas");

            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "empresas",
                newName: "telefone2");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "empresas",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.AddColumn<string>(
                name: "bairro",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cep",
                table: "empresas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cidade",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "complemento",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lote",
                table: "empresas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "numero",
                table: "empresas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quadra",
                table: "empresas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rua",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telefone1",
                table: "empresas",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "uf",
                table: "empresas",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bairro",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "cep",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "cidade",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "complemento",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "lote",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "quadra",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "rua",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "telefone1",
                table: "empresas");

            migrationBuilder.DropColumn(
                name: "uf",
                table: "empresas");

            migrationBuilder.RenameColumn(
                name: "telefone2",
                table: "empresas",
                newName: "telefone");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "empresas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "cnpj",
                table: "empresas",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "endereco_id",
                table: "empresas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_empresas_endereco_id",
                table: "empresas",
                column: "endereco_id");

            migrationBuilder.AddForeignKey(
                name: "FK_empresas_enderecos_endereco_id",
                table: "empresas",
                column: "endereco_id",
                principalTable: "enderecos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
