using System.Collections.Generic;

namespace DeliveryApi.Models
{
    public class Response
    {
        public bool ok { get; set; }
        public string msg { get; set; }
        public List<object> conteudo { get; set; } = new List<object>();
    }
}
