using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdProductVariant { get; set; }

    public int? IdDiscount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public int? Quantity { get; set; }

    public virtual Discount? IdDiscountNavigation { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual ProductVariant? IdProductVariantNavigation { get; set; }
}
