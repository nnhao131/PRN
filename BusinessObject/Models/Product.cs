using System;
using System.Collections.Generic;
using System.IO;

namespace BusinessObject.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public byte[]? Image { get; set; }

    public int? IdCategory { get; set; }

    public bool? Status { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
