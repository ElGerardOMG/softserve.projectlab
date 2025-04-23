using API.DTOs;
using API.implementations.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        private readonly IOrderDomain _processor;
        public OrderController(IOrderDomain orderProcessor)
        {
            _processor = orderProcessor;
        }

        [HttpPost("Create")]
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
        [HttpGet("Get/{id_order}")]
        public IActionResult GetOrder(int id_order)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrder(id_order)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpDelete("Cancel/{id_order}")]
        public IActionResult CancelOrder(int id_order)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.CancelOrder(id_order)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("Status/{id_order}")]
        public IActionResult GetOrderStatus(int id_order)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrderStatus(id_order)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpPut("UpdateStatus/{id_order}")]
        public IActionResult UpdateOrderStatus(int id_order, string status)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.UpdateOrderStatus(id_order, status)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
        [HttpGet("GetOrdersByUser/{id_user}")]
        public IActionResult GetOrdersByUser(int id_user)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.GetOrdersByUser(id_user)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

    }

}
