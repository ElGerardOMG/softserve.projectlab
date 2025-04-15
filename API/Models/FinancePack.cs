using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancePack
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IntervalType { get; set; }

    public double? Interest { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<FinancePackInterval> FinancePackIntervals { get; set; } = new List<FinancePackInterval>();

    public virtual ICollection<FinancedOrder> FinancedOrders { get; set; } = new List<FinancedOrder>();

    public virtual IntervalType? IntervalTypeNavigation { get; set; }
}
