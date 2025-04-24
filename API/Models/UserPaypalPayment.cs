using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserPaypalPayment : BaseClass
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? CardType { get; set; }

    public string? CardNumber { get; set; }

    public string? CardName { get; set; }

    public int? CardExpirationYear { get; set; }

    public int? CardExpirationMonth { get; set; }

    public bool? LastUsed { get; set; }

    public virtual User? User { get; set; }
}
