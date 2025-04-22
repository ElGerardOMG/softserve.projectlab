using System.ComponentModel.DataAnnotations;

namespace API.Models.Authentication
{
    public class SignUpDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Username {get; set; }

    }
}
