using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Assign
{
    public int AssignedId { get; set; }

    public int? DriverDetails { get; set; }

    public int? OrderId { get; set; }

    public string? Destination { get; set; }

    public string? VehicleNum { get; set; }

    public virtual Driver? DriverDetailsNavigation { get; set; }

    public virtual Order? Order { get; set; }
}
