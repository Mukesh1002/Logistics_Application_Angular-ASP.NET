using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string? VehicleName { get; set; }

    public string? VehicleNumber { get; set; }

    public string? VehicleAvailabilityStatus { get; set; }
}
