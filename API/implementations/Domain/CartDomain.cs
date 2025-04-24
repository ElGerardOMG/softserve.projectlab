using API.Data;
using API.implementations.Interfaces;
using API.Models;
using API.DTOs;
using API.Utils.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace API.implementations.Domain
{
    public class CartDomain : ICartDomain
    {
        private readonly ProjectlabContext _db;
        public CartDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ResultDTO CreateCart(int id_user)
        {
            User user = _db.Users.FirstOrDefault(c => c.Id == id_user);
            if (user == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "User not found",
                };
            }
            Cart cart = _db.Cart.FirstOrDefault(c => c.Id == id_user);
            string msg = "Cart already created";
            if (cart == null)
            {
                msg = "Cart succesfully created";
                cart = new Cart
                {
                    IdUser = id_user,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsActive = true
                };
                _db.Cart.Add(cart);
                _db.SaveChanges();
            }
            return new ResultDTO
            {
                statusCode = 201,
                description = msg,
                data = cart,
            };

        }

        public ResultDTO AddToCart(CartItemDTO obj)
        {
            var cart = _db.Cart.FirstOrDefault(c => c.Id == obj.IdCart);
            if (cart == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Cart not found",
                    data = null,
                    error = null
                };
            }
            var product = _db.ProductVariants.FirstOrDefault(p => p.Id == obj.IdProductVariant);
            if (product == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product not found",
                    data = null,
                    error = null
                };
            }
            CartDetail cartItem = _db.CartDetail.FirstOrDefault(ci => ci.IdCart == obj.IdCart && ci.IdProductVariant == obj.IdProductVariant);
            if (cartItem != null)
            {
                cartItem.Quantity += obj.Quantity;
                if (cartItem.Quantity < 1)
                {
                    _db.CartDetail.Remove(cartItem);
                }
                else
                {
                    _db.CartDetail.Update(cartItem);
                }
            }
            else
            {
                _db.CartDetail.Add(DtoMapper.Mapper<CartItemDTO, CartDetail>(obj, true));
            }
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product succesfully added to cart",
                data = null,
                error = null
            };
        }

        public ResultDTO RemoveFromCart(int idCart, int idProductVariant)
        {
            var cartItem = _db.CartDetail.FirstOrDefault(ci => ci.IdCart == idCart && ci.IdProductVariant == idProductVariant);
            if (cartItem == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product not found in cart",
                    data = null,
                    error = null
                };
            }
            _db.CartDetail.Remove(cartItem);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product succesfully removed from cart",
                data = null,
                error = null
            };
        }

        public ResultDTO UpdateQuantity(int idCart, int idProductVariant, int quantity)
        {
            var cartItem = _db.CartDetail.FirstOrDefault(ci => ci.IdCart == idCart && ci.IdProductVariant == idProductVariant);
            if (cartItem == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product not found in cart",
                    data = null,
                    error = null
                };
            }
            if (quantity < 1)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product quantity already is 0",
                    data = null,
                    error = null
                };
            }
            if(cartItem.Quantity == quantity)
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null,
                    error = null
                };
            }
            cartItem.Quantity = quantity;
            _db.CartDetail.Update(cartItem);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product quantity succesfully updated",
                data = null,
                error = null
            };
        }

        public ResultDTO AddQuantity(int idCart, int idProductVariant, int quantity)
        {
            var cartItem = _db.CartDetail.FirstOrDefault(ci => ci.IdCart == idCart && ci.IdProductVariant == idProductVariant);
            if (cartItem == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product not found in cart",
                    data = null,
                    error = null
                };
            }
            if(quantity == 0)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "0 is an invalid value to add",
                    data = null,
                    error = null
                };
            }
            cartItem.Quantity += quantity;
            if (cartItem.Quantity < 1)
            {
                _db.CartDetail.Remove(cartItem);
            }
            else
            {
                _db.CartDetail.Update(cartItem);
            }

            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product quantity succesfully added",
                data = null,
                error = null
            };
        }

        public ResultDTO ClearCart(int idUser)
        {
            var cart = _db.Cart.FirstOrDefault(c => c.Id == idUser);
            if (cart == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Cart no found",
                    data = null,
                    error = null
                };
            }
            var cartItems = _db.CartDetail.Where(ci => ci.IdCart == cart.Id).ToList();
            _db.CartDetail.RemoveRange(cartItems);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Cart succesfully cleared",
                data = null,
                error = null
            };
        }

        public ResultDTO GetCart(int idUser)
        {
            //full cart and cart detail
            Cart cart = _db.Cart.FirstOrDefault(c => c.Id == idUser);
            if (cart == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Cart not found",
                    data = null,
                    error = null
                };
            }
            List<CartDetail> cartDetail = _db.CartDetail.Where(c => c.IdCart == cart.Id).ToList();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Cart detail",
                data = cart,
                error = null
            };
        }

        public ResultDTO GetCartId(int idUser)
        {
            User user = _db.Users.FirstOrDefault(c => c.Id == idUser);
            if (user == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "User not found",
                    data = null,
                    error = null
                };
            }
            Cart cart = _db.Cart.FirstOrDefault(c => c.Id == idUser);
            if (cart == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Cart not found",
                    data = null,
                    error = null
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Cart detail",
                data = cart.Id,
                error = null
            };
        }
    }
}
