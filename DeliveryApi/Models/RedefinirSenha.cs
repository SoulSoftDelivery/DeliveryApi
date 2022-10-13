using System.Collections.Generic;

namespace DeliveryApi.Models
{
    public class RedefinirSenha
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
        public string Token { get; set; }
    }
}
