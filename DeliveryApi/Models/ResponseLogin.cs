using System;
using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class ResponseLogin
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int EmpresaId { get; set; }
        public int TipoUsuarioId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

}
