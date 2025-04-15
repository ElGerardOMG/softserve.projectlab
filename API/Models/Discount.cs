using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Discount
{
    public int Id { get; set; }

    public bool? IsPercentual { get; set; }

    public bool? IsConstant { get; set; }

    public bool? IsCashback { get; set; }

    public bool? IsPrimeOnly { get; set; }

    public double? Value { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }
}
