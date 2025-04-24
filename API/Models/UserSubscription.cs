using System;
using System.Collections.Generic;

namespace API.Models;

public partial class UserSubscription : BaseClass
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
