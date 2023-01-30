using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApi.Migrations
{
    public partial class NewColumnProdutoImgCapaUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "img_capa_url",
                table: "produtos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_capa_url",
                table: "produtos");
        }
    }
}
