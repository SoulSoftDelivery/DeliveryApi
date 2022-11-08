using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class SituacaoPedidoModel : BaseEntity
    {
        [Column("nome")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
    }
}
