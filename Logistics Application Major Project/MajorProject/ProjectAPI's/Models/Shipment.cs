using System;
using System.Collections.Generic;

namespace ProjectAPI_s.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int? OrderId { get; set; }

    public int? OrderDetailsId { get; set; }

    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public string? CurrentStatus { get; set; }

    public DateOnly? OrderPlacedDate { get; set; }

    public DateOnly? ExpectedDelivery { get; set; }
}
