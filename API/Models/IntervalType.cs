using System;
using System.Collections.Generic;

namespace API.Models;

public partial class IntervalType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? TimeInDays { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<FinancePack> FinancePacks { get; set; } = new List<FinancePack>();
}
