using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ProductVariant : BaseClass
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public string? SubName { get; set; }

    public string? Sku { get; set; }

    public string? SubFamily { get; set; }

    public double? Price { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Product? IdProductNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
