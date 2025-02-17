using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class OrderDTO
    {

        
        public int Id { get; set; }

        public string UserName { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? DateOrder { get; set; }

        public string? OrderStatus { get; set; }
      
        //orderdetail
        public int? IdOrder { get; set; }

        public int? IdProduct { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public string? Comment { get; set; }

    }
}
