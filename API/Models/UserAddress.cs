using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserAddress : BaseClass
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
