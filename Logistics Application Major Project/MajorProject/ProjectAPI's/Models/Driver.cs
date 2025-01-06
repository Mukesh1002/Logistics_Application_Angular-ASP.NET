using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPassword { get; set; }

    public string? DriverPhoneNo { get; set; }

    public virtual ICollection<Assign> Assigns { get; set; } = new List<Assign>();

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();
}
