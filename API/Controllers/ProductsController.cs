using API.implementations.Interfaces;
using API.Models;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        public PaginatedResponseDTO<Product> GetProduct(
            [FromBody] FilterDTO? filters,
            string type,
            int page = 1, 
            int pageSize = 10,
            bool onlyIsActive = true)
        {
            return _productProcessor.GetAllProducts(onlyIsActive, filters, type, page, pageSize);
        }

        [HttpGet("GetById/{id}")]
        public async Task<Product>? GetProduct(int id)
        {
            return await _productProcessor.GetProductByID(id);
        }

        [HttpPost("Create")]
        public bool AddProduct([FromBody] ProductDTO obj)
        {
            return _productProcessor.AddProduct(obj);
        }

        [HttpPut("Update/{id}")]
        public bool UpdateProduct(int id, [FromBody] ProductDTO value)
        {
            return _productProcessor.UpdateProduct(id, value);
        }

        [HttpDelete("Delete/{id}")]
        public bool DeleteProduct(int id)
        {
            return _productProcessor.DeleteProduct(id);
        }
    }
}
