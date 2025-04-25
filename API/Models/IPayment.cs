using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public interface IPayment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public bool? LastUsed { get; set; }
    }
}
