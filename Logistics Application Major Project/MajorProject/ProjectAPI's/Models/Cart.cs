using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public decimal? ItemPrice { get; set; }

    public decimal? TotalPriceOfItem { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Inventory? Item { get; set; }
}
