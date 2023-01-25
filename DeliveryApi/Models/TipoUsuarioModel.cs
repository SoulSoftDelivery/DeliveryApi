using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class MesaModel : BaseEntity
    {
        [Column("numero")]
        [Required]
        public int Numero { get; set; }
        [ForeignKey("Empresa")]
        [Required]
        public int EmpresaId { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
    }
}
