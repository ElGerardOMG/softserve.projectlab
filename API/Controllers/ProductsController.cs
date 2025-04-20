using API.implementations.Interfaces;
using API.Models;
using API.DTOs;
using API.Utils;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // PRODUCT CONTROLLER

        private readonly IProductDomain _productProcessor;
        public ProductsController(IProductDomain productProcessor)
        {
            _productProcessor = productProcessor;
        }
        // PRODUCTS

        [HttpPost("GetFiltered/{type}")]
        public IActionResult GetProduct(
            [FromBody] FilterDTO? filters,
            string type,
            int page = 1, 
            int pageSize = 10,
            bool onlyIsActive = true)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetAllProducts(onlyIsActive, filters, type, page, pageSize)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetProductByID(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("Create")]
        public IActionResult AddProduct([FromBody] ProductDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.AddProduct(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDTO value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.UpdateProduct(id, value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.DeleteProduct(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
