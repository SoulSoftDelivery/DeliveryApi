using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("dt_cadastro")]
        [Required]
        public DateTime DtCadastro { get; set; }
        [Column("dt_atualizacao")]
        [Required]
        public DateTime DtAtualizacao { get; set; }
        [Column("ativo")]
        [Required]
        public bool Ativo { get; set; }
    }
}
