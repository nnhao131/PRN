using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } // Thêm thông tin chi tiết tên sản phẩm
        public decimal? Price { get; set; }
       
        public byte[]? Image { get; set; }
    }
}
