using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public decimal? ItemPrice { get; set; }

    public decimal? TotalItemPrice { get; set; }

    public DateOnly? ExpectedDelivery { get; set; }

    public virtual Inventory? Item { get; set; }

    public virtual Order? Order { get; set; }
}
