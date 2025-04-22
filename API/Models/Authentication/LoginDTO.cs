using System.ComponentModel.DataAnnotations;

namespace API.Models.Authentication
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
