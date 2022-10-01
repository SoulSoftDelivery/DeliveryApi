using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliveryApi.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias_produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias_produtos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "erros",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_code = table.Column<int>(type: "int", nullable: false),
                    nome_aplicacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome_funcao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    parametro_entrada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    descricao_completa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registro_corrente_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_erros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "situacoes_pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_situacoes_pedidos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_enderecos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_enderecos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_medidas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_medidas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_pedidos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enderecos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    quadra = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    lote = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    cliente_id = table.Column<int>(type: "int", nullable: false),
                    tipo_endereco_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enderecos", x => x.id);
                    table.ForeignKey(
                        name: "FK_enderecos_tipos_enderecos_tipo_endereco_id",
                        column: x => x.tipo_endereco_id,
                        principalTable: "tipos_enderecos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    endereco_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                    table.ForeignKey(
                        name: "FK_empresas_enderecos_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "enderecos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    senha = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: true),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_clientes_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    qtd = table.Column<int>(type: "int", nullable: false),
                    valor = table.Column<double>(type: "float", nullable: false),
                    categoria_produto_id = table.Column<int>(type: "int", nullable: false),
                    tipo_medida_id = table.Column<int>(type: "int", nullable: false),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.id);
                    table.ForeignKey(
                        name: "FK_produtos_categorias_produtos_categoria_produto_id",
                        column: x => x.categoria_produto_id,
                        principalTable: "categorias_produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_produtos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_produtos_tipos_medidas_tipo_medida_id",
                        column: x => x.tipo_medida_id,
                        principalTable: "tipos_medidas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dt_ultimo_acesso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    tipo_usuario_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usuarios_tipos_usuarios_tipo_usuario_id",
                        column: x => x.tipo_usuario_id,
                        principalTable: "tipos_usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valor_total = table.Column<double>(type: "float", nullable: false),
                    cliente_id = table.Column<int>(type: "int", nullable: false),
                    endereco_id = table.Column<int>(type: "int", nullable: false),
                    tipo_pedido_id = table.Column<int>(type: "int", nullable: false),
                    situacao_pedido_id = table.Column<int>(type: "int", nullable: false),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    situacao = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "clientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_enderecos_endereco_id",
                        column: x => x.endereco_id,
                        principalTable: "enderecos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_situacoes_pedidos_situacao_pedido_id",
                        column: x => x.situacao_pedido_id,
                        principalTable: "situacoes_pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_tipos_pedidos_tipo_pedido_id",
                        column: x => x.tipo_pedido_id,
                        principalTable: "tipos_pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedidos_produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pedido_id = table.Column<int>(type: "int", nullable: false),
                    produto_id = table.Column<int>(type: "int", nullable: false),
                    qtd = table.Column<int>(type: "int", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos_produtos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_produtos_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedidos_produtos_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clientes_empresa_id",
                table: "clientes",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_empresas_endereco_id",
                table: "empresas",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_enderecos_tipo_endereco_id",
                table: "enderecos",
                column: "tipo_endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_cliente_id",
                table: "pedidos",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_empresa_id",
                table: "pedidos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_endereco_id",
                table: "pedidos",
                column: "endereco_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_situacao_pedido_id",
                table: "pedidos",
                column: "situacao_pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_tipo_pedido_id",
                table: "pedidos",
                column: "tipo_pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_produtos_pedido_id",
                table: "pedidos_produtos",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_produtos_produto_id",
                table: "pedidos_produtos",
                column: "produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_categoria_produto_id",
                table: "produtos",
                column: "categoria_produto_id");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_empresa_id",
                table: "produtos",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_tipo_medida_id",
                table: "produtos",
                column: "tipo_medida_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_empresa_id",
                table: "usuarios",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_tipo_usuario_id",
                table: "usuarios",
                column: "tipo_usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "erros");

            migrationBuilder.DropTable(
                name: "pedidos_produtos");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "tipos_usuarios");

            migrationBuilder.DropTable(
                name: "clientes");

            migrationBuilder.DropTable(
                name: "situacoes_pedidos");

            migrationBuilder.DropTable(
                name: "tipos_pedidos");

            migrationBuilder.DropTable(
                name: "categorias_produtos");

            migrationBuilder.DropTable(
                name: "tipos_medidas");

            migrationBuilder.DropTable(
                name: "empresas");

            migrationBuilder.DropTable(
                name: "enderecos");

            migrationBuilder.DropTable(
                name: "tipos_enderecos");
        }
    }
}
