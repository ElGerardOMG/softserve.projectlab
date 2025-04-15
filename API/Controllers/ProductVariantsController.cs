using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVariantsController
    {
        // PRODUCT VARIANT CONTROLLER

        private readonly IProductVariantDomain _productProcessor;
        public ProductVariantsController(IProductVariantDomain productProcessor)
        {
            _productProcessor = productProcessor;
        }
        // PRODUCT VARIANTS

        [HttpPost("GetById/{id}")]
        public ProductVariant? GetProductVariantById(int id)
        {
            return _productProcessor.GetProductVariantById(id);
        }

        [HttpPost("Create")]
        public bool AddProductVariant([FromBody] ProductVariantDTO obj)
        {
            return _productProcessor.AddProductVariant(obj);
        }

        [HttpPut("Update/{id}")]
        public bool UpdateProductVariant(int id, [FromBody] ProductVariantDTO obj)
        {
            return _productProcessor.UpdateProductVariant(id, obj);
        }

        [HttpDelete("Delete/{id}")]
        public bool DeleteProductVariant(int id)
        {
            return _productProcessor.DeleteProductVariant(id);
        }
    }
}
