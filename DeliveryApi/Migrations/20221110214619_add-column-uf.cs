using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApi.Migrations
{
    public partial class addcolumnuf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "uf",
                table: "enderecos",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uf",
                table: "enderecos");
        }
    }
}
