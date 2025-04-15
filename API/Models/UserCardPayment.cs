using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserCardPayment
{
    public int Id { get; set; }

    public int? IdUserPayment { get; set; }

    public string? CardType { get; set; }

    public string? CardNumber { get; set; }

    public string? CardName { get; set; }

    public int? CardExpirationYear { get; set; }

    public int? CardExpirationMonth { get; set; }

    public bool? LastUsed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? IdUserPaymentNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
