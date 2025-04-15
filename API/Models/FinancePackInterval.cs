using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancePackInterval
{
    public int Id { get; set; }

    public int? IdFinancePack { get; set; }

    public int? IntervalCount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual FinancePack? IdFinancePackNavigation { get; set; }
}
