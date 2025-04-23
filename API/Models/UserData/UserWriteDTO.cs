using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models.UserData
{
    // UserDTO used for User read/Write interaction
    public class UserWriteDTO
    { 
        public string UserName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Lastname { get; set; }

   
    }
}
