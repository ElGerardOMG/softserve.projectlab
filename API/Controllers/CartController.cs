using API.DTOs;
using API.implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartDomain _processor;
        public CartController(ICartDomain attributeProcessor)
        {
            _processor = attributeProcessor;
        }

        [HttpPost]
        public IActionResult CreateCart(int idUser)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CreateCart(idUser)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartItemDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.AddToCart(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("{idCart}/RemoveItem/{idProduct}")]
        public IActionResult RemoveFromCart(int idCart, int idProduct)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.RemoveFromCart(idCart, idProduct)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPatch("{idCart}/AddQuantity/{idProduct}")]
        public IActionResult AddQuantity(int idCart, int idProduct, int quantity)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.AddQuantity(idCart, idProduct, quantity)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPatch("{idCart}/UpdateQuantity/{idProduct}")]
        public IActionResult UpdateQuantity(int idCart, int idProduct, int quantity)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateQuantity(idCart, idProduct, quantity)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpDelete("Clear/{id_cart}")]
        public IActionResult ClearCart(int idCart)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.ClearCart(idCart)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("{id_cart}")]
        public IActionResult GetCart(int idCart)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetCart(idCart)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpGet("GetCartId/{idUser}")]
        public IActionResult GetCartId(int idUser)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetCartId(idUser)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
