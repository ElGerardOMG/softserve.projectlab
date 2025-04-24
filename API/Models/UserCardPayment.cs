using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserCardPayment : BaseClass
{
    public int Id { get; set; }

    public int? IdUserPayment { get; set; }

    public string? CardType { get; set; }

    public string? CardNumber { get; set; }

    public string? CardName { get; set; }

    public int? CardExpirationYear { get; set; }

    public int? CardExpirationMonth { get; set; }

    public bool? LastUsed { get; set; }

    public virtual User? IdUserPaymentNavigation { get; set; }
}
