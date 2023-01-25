
using DeliveryApi.Models.Base;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApi.Models
{
    public class CadastrarProduto : BaseEntity
    {
        [StringLength(100)]
        [Required]
        public string Nome { get; set; }
        [StringLength(255)]
        public string Descricao { get; set; }
        public int? Qtd { get; set; }
        [Required]
        public float Valor { get; set; }
        [StringLength(100)]
        public string ImgCapaUrl { get; set; }
        public IFormFile ImgCapa { get; set; }
        [Required]
        public int CategoriaProdutoId { get; set; }
        [Required]
        public int TipoMedidaId { get; set; }
        [Required]
        public int EmpresaId { get; set; }
    }
}
