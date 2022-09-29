using System;
using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class UsuarioModel : BaseEntity
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
        [Column("dt_ultimo_acesso")]
        public DateTime DtUltimoAcesso { get; set; }
        [Column("senha")]
        [StringLength(22)]
        public string Senha { get; set; }
        [Column("empresa_id")]
        [ForeignKey("Empresa")]
        [Required]
        public int EmpresaId { get; set; }
        [Column("tipo_usuario_id")]
        [ForeignKey("TipoUsuario")]
        public int TipoUsuarioId { get; set; }
        public virtual TipoUsuarioModel TipoUsuario { get; set; }
        public virtual EmpresaModel Empresa { get; set; }
    }

}
