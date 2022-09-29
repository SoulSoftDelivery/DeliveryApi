using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class PedidoProdutoModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("pedido_id")]
        [ForeignKey("Pedido")]
        [Required]
        public int PedidoId { get; set; }
        [Column("produto_id")]
        [ForeignKey("Produto")]
        [Required]
        public int ProdutoId { get; set; }
        [Column("qtd")]
        [Required]
        public int Qtd { get; set; }
        [Column("observacao")]
        [StringLength(255)]
        public string Observacao { get; set; }
        [JsonIgnore]
        public virtual PedidoModel Pedido { get; set; }
        public virtual ProdutoModel Produto { get; set; }

    }
}
