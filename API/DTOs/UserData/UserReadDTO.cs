using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.DTOs.UserData
{
    // UserDTO used for User only for read interaction
    public class UserReadDTO
    { 
        public string UserName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Lastname { get; set; }

        [StringLength(255)]
        public string ReferralCode { get; set; }
    }
}
