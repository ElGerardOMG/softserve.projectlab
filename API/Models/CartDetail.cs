using System;
using System.Collections.Generic;

namespace API.Models;

public partial class CartDetail
{
    public int Id { get; set; }

    public int? IdCart { get; set; }

    public int? IdProductVariant { get; set; }

    public int? Quantity { get; set; }

    public virtual Cart? IdCartNavigation { get; set; }

    public virtual ProductVariant? IdProductVariantNavigation { get; set; }
}
