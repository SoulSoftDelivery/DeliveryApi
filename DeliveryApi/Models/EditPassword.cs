using System.ComponentModel.DataAnnotations;

namespace DeliveryApi.Models
{
    public class EditPassword
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string SenhaAtual { get; set; }
        [Required]
        public string NovaSenha { get; set; }
    }

}
