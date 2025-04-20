using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
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
        public async Task<IActionResult> GetProductVariantById(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetProductVariantById(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("Create")]
        public IActionResult AddProductVariant([FromBody] ProductVariantDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.AddProductVariant(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateProductVariant(int id, [FromBody] ProductVariantDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.UpdateProductVariant(id, obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProductVariant(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.DeleteProductVariant(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
