using API.DTOs;
using API.implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController
    {
        private readonly ICartDomain _processor;
        public CartController(ICartDomain attributeProcessor)
        {
            _processor = attributeProcessor;
        }

        [HttpPost("Create")]
        public IActionResult CreateCart(int id_user)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CreateCart(id_user)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("AddItem")]
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

        [HttpDelete("RemoveItem/{id_cart}")]
        public IActionResult RemoveFromCart(int id_cart, int id_product)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.RemoveFromCart(id_cart, id_product)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPatch("AddQuantity/{id_cart}")]
        public IActionResult AddQuantity(int id_cart, int id_product, int quantity)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.AddQuantity(id_cart, id_product, quantity)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPatch("UpdateQuantity/{id_cart}")]
        public IActionResult UpdateQuantity(int id_cart, int id_product, int quantity)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateQuantity(id_cart, id_product, quantity)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpDelete("Clear/{id_cart}")]
        public IActionResult ClearCart(int id_cart)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.ClearCart(id_cart)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("Get/{id_cart}")]
        public IActionResult GetCart(int id_cart)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetCart(id_cart)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpGet("GetId/{id_user}")]
        public IActionResult GetCartId(int id_user)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetCartId(id_user)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
