﻿using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class TipoUsuarioModel : BaseEntity
    {
        [Column("descricao")]
        [StringLength(50)]
        [Required]
        public string Descricao { get; set; }
        //[ForeignKey("Empresa")]
        //[Required]
        //public int EmpresaId { get; set; }
        //public virtual EmpresaModel Empresa { get; set; }
        //public List<UsuarioModel> Usuarios { get; set; }
    }
}
