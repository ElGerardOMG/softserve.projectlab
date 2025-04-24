using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Shipment : BaseClass
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdUser { get; set; }

    public string? ShipmentCompany { get; set; }

    public string? GuideNumber { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public DateTime? EstimatedArrival { get; set; }

    public DateTime? Arrival { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
