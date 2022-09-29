using System;
using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }

}
