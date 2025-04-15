using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Shipment
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdUser { get; set; }

    public string? ShipmentCompany { get; set; }

    public string? GuideNumber { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public DateTime? EstimatedArrival { get; set; }

    public DateTime? Arrival { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
