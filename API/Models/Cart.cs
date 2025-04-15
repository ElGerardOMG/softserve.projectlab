using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
