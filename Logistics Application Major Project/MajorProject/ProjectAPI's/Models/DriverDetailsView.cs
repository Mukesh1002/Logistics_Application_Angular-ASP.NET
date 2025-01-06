using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class DriverDetailsView
{
    public int AssignedId { get; set; }

    public int DriverId { get; set; }

    public string? DriverName { get; set; }

    public int? OrderId { get; set; }

    public string? Destination { get; set; }

    public string? CurrentStatus { get; set; }

    public DateOnly? ExpectedDelivery { get; set; }
}
