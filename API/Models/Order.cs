using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Order : BaseClass
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public double? Subtotal { get; set; }

    public double? Taxes { get; set; }

    public double? Total { get; set; }

    public string? Status { get; set; }

    public int? IdUserAddress { get; set; }

    public string? PaymentType { get; set; }

    public int? IdUserPaymentCard { get; set; }

    public int? IdUserPaymentPaypal { get; set; }

    public bool? IsFinanced { get; set; }

    public int? ReferralUser { get; set; }

    public virtual ICollection<FinancedOrder> FinancedOrders { get; set; } = new List<FinancedOrder>();

    public virtual UserAddress? IdUserAddressNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? ReferralUserNavigation { get; set; }

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
