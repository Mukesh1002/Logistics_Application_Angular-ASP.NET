using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class CartView
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemImg { get; set; }

    public int? InventoryQuantity { get; set; }

    public decimal? Price { get; set; }

    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? CartItemQuantity { get; set; }

    public decimal? TotalPriceOfItem { get; set; }
}
