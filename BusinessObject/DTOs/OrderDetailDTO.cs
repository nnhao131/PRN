using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public decimal? TotalPrice { get; set; }

        public DateTime? DateOrder { get; set; }

        public string? OrderStatus { get; set; }
    }
}
