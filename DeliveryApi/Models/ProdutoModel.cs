
using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class ProdutoModel : BaseEntity
    {
        [Column("nome")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
        [Column("descricao")]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Column("qtd")]
        public int? Qtd { get; set; }
        [Column("valor")]
        [Required]
        public float Valor { get; set; }
        [Column("img_capa_nome")]
        [StringLength(50)]
        public string ImgCapaNome { get; set; }
        [Column("img_capa_url")]
        [StringLength(100)]
        public string ImgCapaUrl { get; set; }
        [Column("categoria_produto_id")]
        [ForeignKey("CategoriaProduto")]
        [Required]
        public int CategoriaProdutoId { get; set; }
        [Column("tipo_medida_id")]
        [ForeignKey("TipoMedida")]
        [Required]
        public int TipoMedidaId { get; set; }
        [Column("empresa_id")]
        [ForeignKey("Empresa")]
        [Required]
        public int EmpresaId { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
        public virtual CategoriaProdutoModel CategoriaProduto { get; set; }
        public virtual TipoMedidaModel TipoMedida { get; set; }
    }
}
