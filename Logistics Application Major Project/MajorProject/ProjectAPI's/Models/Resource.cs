using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Resource
{
    public int ResourceId { get; set; }

    public int? DriverId { get; set; }

    public string? DriverAvailabilityStatus { get; set; }

    public int? CurrentAssignment { get; set; }

    public string? VehicleAllocated { get; set; }

    public virtual Order? CurrentAssignmentNavigation { get; set; }

    public virtual Driver? Driver { get; set; }
}
