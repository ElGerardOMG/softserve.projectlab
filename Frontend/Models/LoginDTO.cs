using System.ComponentModel.DataAnnotations;

namespace Frontend.Models;

public class LoginDTO
{
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
