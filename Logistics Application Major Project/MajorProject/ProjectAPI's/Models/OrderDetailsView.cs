using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class OrderDetailsView
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string? CurrentStatus { get; set; }

    public string? Destination { get; set; }

    public int? OrderDetailsId { get; set; }

    public int? Itemid { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemImg { get; set; }

    public int? ItemQuantity { get; set; }

    public decimal? ItemPrice { get; set; }

    public decimal? TotalItemPrice { get; set; }

    public DateOnly? ExpectedDelivery { get; set; }
}
