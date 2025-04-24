using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancePackInterval : BaseClass
{
    public int Id { get; set; }

    public int? IdFinancePack { get; set; }

    public int? IntervalCount { get; set; }

    public virtual FinancePack? IdFinancePackNavigation { get; set; }
}
