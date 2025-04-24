using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cart : BaseClass
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual User? IdUserNavigation { get; set; }
}
