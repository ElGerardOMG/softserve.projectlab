using System;
using System.Collections.Generic;

namespace API.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public string? SubName { get; set; }

    public string? Sku { get; set; }

    public string? SubFamily { get; set; }

    public double? Price { get; set; }

    public int? Stock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual Product? IdProductNavigation { get; set; }
}
