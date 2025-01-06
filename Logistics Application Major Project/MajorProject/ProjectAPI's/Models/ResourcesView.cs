using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class ResourcesView
{
    public int ResourceId { get; set; }

    public int? Driverid { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPhoneNo { get; set; }

    public string? DriverAvailabilityStatus { get; set; }

    public int? Currentassignment { get; set; }

    public string? VehicleAllocated { get; set; }
}
