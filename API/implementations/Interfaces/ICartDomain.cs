using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface ICartDomain
    {
        public ResultDTO CreateCart(int id_user);
        public ResultDTO AddToCart(CartItemDTO obj);
        public ResultDTO RemoveFromCart(int id_cart, int id_product);
        public ResultDTO AddQuantity(int id_cart, int id_product, int quantity); // positive quantity to ADD, negative to SUBTRACT
        public ResultDTO UpdateQuantity(int id_cart, int id_product, int quantity);
        public ResultDTO ClearCart(int id_user);
        public ResultDTO GetCart(int id_user);
        public ResultDTO GetCartId(int id_user);
    }
}
