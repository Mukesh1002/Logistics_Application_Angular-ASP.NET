using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerPassword { get; set; }

    public string? CustomerPhoneNo { get; set; }

    public string? CustomerAddress { get; set; }

    public decimal? CustomerWallet { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
