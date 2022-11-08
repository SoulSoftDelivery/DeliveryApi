using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class ClienteModel : BaseEntity
    {
        [Column("nome")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
        [Column("telefone")]
        [StringLength(16)]
        [Required]
        public string Telefone { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("senha")]
        [StringLength(22)]
        public string Senha { get; set; }
        [Column("sexo")]
        public char Sexo { get; set; }
        [Column("empresa_id")]
        [ForeignKey("Empresa")]
        [Required]
        public int EmpresaId { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
    }
}