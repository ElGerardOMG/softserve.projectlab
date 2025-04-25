using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserPaypalPayment
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public bool? LastUsed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? Email { get; set; }

    public virtual User? User { get; set; }
}
