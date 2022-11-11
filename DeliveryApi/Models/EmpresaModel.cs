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
        public string Cnpj { get; set; }
        [Column("telefone1")]
        [StringLength(16)]
        [Required]
        public string Telefone1 { get; set; }
        [Column("telefone2")]
        [StringLength(16)]
        public string Telefone2 { get; set; }
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Column("uf")]
        [StringLength(2)]
        [Required]
        public string Uf { get; set; }
        [Column("cidade")]
        [StringLength(100)]
        [Required]
        public string Cidade { get; set; }
        [Column("cep")]
        [StringLength(10)]
        public string Cep { get; set; }
        [Column("bairro")]
        [StringLength(100)]
        public string Bairro { get; set; }
        [Column("rua")]
        [StringLength(100)]
        public string Rua { get; set; }
        [Column("quadra")]
        [StringLength(10)]
        public string Quadra { get; set; }
        [Column("lote")]
        [StringLength(10)]
        public string Lote { get; set; }
        [Column("numero")]
        [StringLength(10)]
        public string Numero { get; set; }
        [Column("complemento")]
        [StringLength(100)]
        public string Complemento { get; set; }
    }
}
