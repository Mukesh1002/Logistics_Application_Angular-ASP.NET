using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Inventory
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemImg { get; set; }

    public int? Quantity { get; set; }

    public string? WarehouseLocation { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
