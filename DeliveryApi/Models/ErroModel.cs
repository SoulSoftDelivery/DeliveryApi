using DeliveryApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApi.Models
{
    public class ErroModel : BaseEntity
    {
        [Column("status_code")]
        public int StatusCode { get; set; }
        [Column("nome_aplicacao")]
        public string NomeAplicacao { get; set; }
        [Column("nome_funcao")]
        public string NomeFuncao { get; set; }
        [Column("url")]
        public string Url { get; set; }
        [Column("parametro_entrada")]
        public string ParametroEntrada { get; set; }
        [Column("descricao")]
        [StringLength(255)]
        public string Descricao { get; set; }
        [Column("descricao_completa")]
        public string DescricaoCompleta { get; set; }
        [Column("registro_corrente_id")]
        public int RegistroCorrenteId { get; set; }
    }
}
