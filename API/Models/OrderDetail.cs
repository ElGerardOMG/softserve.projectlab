using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderDetail
{
    public int? IdOrder { get; set; }

    public int? IdProduct { get; set; }

    public int? IdDiscount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Discount? IdDiscountNavigation { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
