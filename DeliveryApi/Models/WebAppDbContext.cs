using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeliveryApi.Models
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
        }
        public DbSet<TipoEnderecoModel> tipos_enderecos { get; set; }
        public DbSet<CategoriaProdutoModel> categorias_produtos { get; set; }
        public DbSet<TipoMedidaModel> tipos_medidas { get; set; }
        public DbSet<TipoUsuarioModel> tipos_usuarios { get; set; }
        public DbSet<SituacaoPedidoModel> situacoes_pedidos { get; set; }
        public DbSet<TipoPedidoModel> tipos_pedidos { get; set; }
        public DbSet<EmpresaModel> empresas { get; set; }
        public DbSet<ClienteModel> clientes { get; set; }
        public DbSet<EnderecoModel> enderecos { get; set; }
        public DbSet<UsuarioModel> usuarios { get; set; }
        public DbSet<ProdutoModel> produtos { get; set; }
        public DbSet<PedidoModel> pedidos { get; set; }
        public DbSet<MesaModel> mesas { get; set; }
        public DbSet<PedidoProdutoModel> pedidos_produtos { get; set; }
        public DbSet<ErroModel> erros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
