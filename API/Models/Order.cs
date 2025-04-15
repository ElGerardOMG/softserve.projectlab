using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public double? Subtotal { get; set; }

    public double? Taxes { get; set; }

    public double? Total { get; set; }

    public int? IdUserAddress { get; set; }

    public string? PaymentType { get; set; }

    public int? IdUserPaymentCard { get; set; }

    public int? IdUserPaymentPaypal { get; set; }

    public bool? IsFinanced { get; set; }

    public int? ReferralUser { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<FinancedOrder> FinancedOrders { get; set; } = new List<FinancedOrder>();

    public virtual UserAddress? IdUserAddressNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual UserCardPayment? IdUserPaymentCardNavigation { get; set; }

    public virtual UserPaypalPayment? IdUserPaymentPaypalNavigation { get; set; }

    public virtual User? ReferralUserNavigation { get; set; }

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
