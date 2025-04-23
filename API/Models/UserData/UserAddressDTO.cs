using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.UserData;


public class UserAddressDTO
{
    [StringLength(255)]
    public string Address1 { get; set; }


    [StringLength(255)]
    public string Address2 { get; set; }

    [StringLength(255)]
    public string PostalCode { get; set; }

    public string Country { get; set; }

    [StringLength(255)]
    public string State { get; set; }

    [StringLength(255)]
    public string City { get; set; }

}
