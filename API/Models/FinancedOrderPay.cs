using System;
using System.Collections.Generic;

namespace API.Models;

public partial class FinancedOrderPay : BaseClass
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdPaymentNumber { get; set; }

    public double? PaymentAmount { get; set; }

    public double? TotalPayed { get; set; }

    public DateTime? LimitPayDate { get; set; }

    public DateTime? PayDate { get; set; }

    public virtual FinancedOrder? IdOrderNavigation { get; set; }
}
