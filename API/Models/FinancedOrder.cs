using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancedOrder
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdFinancePack { get; set; }

    public double? Amount { get; set; }

    public double? TotalInterest { get; set; }

    public bool? IsPayed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<FinancedOrderPay> FinancedOrderPays { get; set; } = new List<FinancedOrderPay>();

    public virtual FinancePack? IdFinancePackNavigation { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }
}
