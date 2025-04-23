using API.Models;

namespace API.DTOs
{
    public class CartItemDTO
    {
        public int? IdCart { get; set; }

        public int? IdProductVariant { get; set; }

        public int? Quantity { get; set; }
    }
}
