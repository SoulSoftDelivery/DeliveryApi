using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class EmpresaModel : BaseEntity
    {
        [Column("nome")]
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
        [Column("cnpj")]
        [StringLength(18)]
        [Required]
        public string Cnpj { get; set; }
        [Column("telefone")]
        [StringLength(16)]
        public string Telefone { get; set; }
        [Column("email")]
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [Column("endereco_id")]
        [ForeignKey("Endereco")]
        [Required]
        public int EnderecoId { get; set; }
        public virtual EnderecoModel Endereco {get; set;}
        
        //public List<UsuarioModel> Usuarios { get; set; }
    }
}
