using API.Models;

namespace API.DTOs
{
    public class CartDetailDTO
    {
        public Cart cart { get; set; }
        public List<CartDetail> cartDetails { get; set; }
    }
}
