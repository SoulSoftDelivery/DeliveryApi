﻿using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class TipoPedidoModel : BaseEntity
    {
        [Column("descricao")]
        [StringLength(100)]
        [Required]
        public string Descricao { get; set; }
    }
}
