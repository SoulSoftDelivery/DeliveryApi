using System.ComponentModel.DataAnnotations;

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
