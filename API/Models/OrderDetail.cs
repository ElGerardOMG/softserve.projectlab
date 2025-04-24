using System;
using System.Collections.Generic;

namespace API.Models;

public partial class OrderDetail : BaseClass
{
    public int Id { get; set; }

    public int? IdOrder { get; set; }

    public int? IdProductVariant { get; set; }

    public int? IdDiscount { get; set; }

    public int? Quantity { get; set; }

    public virtual Discount? IdDiscountNavigation { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual ProductVariant? IdProductVariantNavigation { get; set; }
}
