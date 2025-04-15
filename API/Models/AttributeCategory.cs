using System;
using System.Collections.Generic;

namespace API.Models;

public partial class AttributeCategory : BaseClass
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();
}
