using API.DTOs;
using API.implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDomain _processor;
        public OrderController(IOrderDomain orderProcessor)
        {
            _processor = orderProcessor;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CreateOrder(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("{idOrder}")]
        public IActionResult GetOrder(int idOrder)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrder(idOrder)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpDelete("{idOrder}")]
        public IActionResult CancelOrder(int idOrder)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CancelOrder(idOrder)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("GetStatus/{idOrder}")]
        public IActionResult GetOrderStatus(int idOrder)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrderStatus(idOrder)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPatch("{idOrder}")]
        public IActionResult UpdateOrderStatus(int idOrder, string status)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateOrderStatus(idOrder, status)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("GetOrdersByUser/{idUser}")]
        public IActionResult GetOrdersByUser(int idUser)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrdersByUser(idUser, new FilterDTO())
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

    }

}
