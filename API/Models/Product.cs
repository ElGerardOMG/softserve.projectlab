using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product : BaseClass
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ProductType { get; set; }

    public int? ProductCategory { get; set; }

    public string? Brand { get; set; }

    public string? Family { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ProductCategory? ProductCategoryNavigation { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
