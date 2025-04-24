using System;
using System.Collections.Generic;

namespace API.Models;

public partial class IntervalType : BaseClass
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? TimeInDays { get; set; }

    public virtual ICollection<FinancePack> FinancePacks { get; set; } = new List<FinancePack>();
}
