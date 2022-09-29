using DeliveryApi.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class PedidoModel : BaseEntity
    {
        [Column("valor_total")]
        [Required]
        public double ValorTotal { get; set; }
        [Column("cliente_id")]
        [ForeignKey("Cliente")]
        [Required]
        public int ClienteId { get; set; }
        [Column("endereco_id")]
        [ForeignKey("Endereco")]
        [Required]
        public int EnderecoId { get; set; }
        [Column("tipo_pedido_id")]
        [ForeignKey("TipoPedido")]
        [Required]
        public int TipoPedidoId { get; set; }
        [Column("situacao_pedido_id")]
        [ForeignKey("SituacaoPedido")]
        [Required]
        public int SituacaoPedidoId { get; set; }
        [Column("empresa_id")]
        [ForeignKey("Empresa")]
        [Required]
        public int EmpresaId { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
        public virtual TipoPedidoModel TipoPedido { get; set; }
        public virtual SituacaoPedidoModel SituacaoPedido { get; set; }
        public virtual ClienteModel Cliente { get; set; }
        public virtual EnderecoModel Endereco { get; set; }
        public virtual List<PedidoProdutoModel> PedidoProdutos { get; set; }
    }
}
