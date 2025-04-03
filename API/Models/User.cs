using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(64)]
        public string UserName { get; set; }
        [MaxLength(64)]
        public string Password { get; set; }
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(64)]
        public string Phone { get; set; }
        [MaxLength(127)]
        public string Name { get; set; }
        [MaxLength(127)]
        public string Lastname { get; set; }
        public string Last_used_payment_type { get; set; }
        public int Last_used_payment { get; set; }
        DateTime created_at { get; set; }
        DateTime update_at { get; set; }
        DateTime deleted_at { get; set; }
        DateTime is_active { get; set; }


    }
}
