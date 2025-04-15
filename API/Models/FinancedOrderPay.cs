using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancedOrderPay
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdPaymentNumber { get; set; }

    public double? PaymentAmount { get; set; }

    public double? TotalPayed { get; set; }

    public DateTime? LimitPayDate { get; set; }

    public DateTime? PayDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual FinancedOrder? IdOrderNavigation { get; set; }
}
