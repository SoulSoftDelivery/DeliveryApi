using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class EnderecoModel : BaseEntity
    {
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
        //[Required]
        public string Cep { get; set; }
        [Column("bairro")]
        [StringLength(100)]
        [Required]
        public string Bairro { get; set; }
        [Column("rua")]
        [StringLength(100)]
        [Required]
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
        [Column("cliente_id")]
        public int ClienteId { get; set; }
        [Column("tipo_endereco_id")]
        [ForeignKey("TipoEndereco")]
        public int TipoEnderecoId { get; set; }
        public virtual TipoEnderecoModel TipoEndereco { get;set; }
    }
}
