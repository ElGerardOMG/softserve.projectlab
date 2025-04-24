using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancePack : BaseClass
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IntervalType { get; set; }

    public double? Interest { get; set; }

    public virtual ICollection<FinancePackInterval> FinancePackIntervals { get; set; } = new List<FinancePackInterval>();

    public virtual ICollection<FinancedOrder> FinancedOrders { get; set; } = new List<FinancedOrder>();

    public virtual IntervalType? IntervalTypeNavigation { get; set; }
}
