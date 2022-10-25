using System.Collections.Generic;

namespace DeliveryApi.Models
{
    public class PaginationResponse
    {
        public List<object> Results { get; set; } = new List<object>();
        public int TotalPages { get; set; }
        public int TotalRows { get; set; }
    }
}
