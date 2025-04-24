using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Attribute : BaseClass
{
    public int Id { get; set; }

    public int? IdProduct { get; set; }

    public int? IdProductVariant { get; set; }

    public int? IdAttributeCategory { get; set; }

    public string? Field { get; set; }

    public string? Value { get; set; }

    public virtual AttributeCategory? IdAttributeCategoryNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual ProductVariant? IdProductVariantNavigation { get; set; }
}
