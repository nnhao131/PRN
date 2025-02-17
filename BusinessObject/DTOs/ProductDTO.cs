﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        public byte[]? Image { get; set; }

        public string? CategoryName { get; set; }

        public bool? Status { get; set; }
        public int? cateid { get; set; }
    }
}
