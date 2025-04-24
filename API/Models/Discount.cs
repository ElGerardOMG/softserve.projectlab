using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Discount : BaseClass
{
    public int Id { get; set; }

    public bool? IsPercentual { get; set; }

    public bool? IsConstant { get; set; }

    public bool? IsCashback { get; set; }

    public bool? IsPrimeOnly { get; set; }

    public double? Value { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
