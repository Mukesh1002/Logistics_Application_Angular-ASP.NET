using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string? OrderStatus { get; set; }

    public string? Destination { get; set; }

    public DateTime? OrderPlacedDate { get; set; }

    public decimal? TotalBillAmount { get; set; }

    public virtual ICollection<Assign> Assigns { get; set; } = new List<Assign>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}
