using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class TipoMedidaModel : BaseEntity
    {
        [Column("nome")]
        [StringLength(50)]
        [Required]
        public string Nome { get; set; }
        //[ForeignKey("Empresa")]
        //[Required]
        //public int EmpresaId { get; set; }
        //public virtual EmpresaModel Empresa { get; set; }
        //public List<ProdutoModel> Produtos { get; set; }
    }
}
