using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductVariantsController : ControllerBase
    {
        // PRODUCT VARIANT CONTROLLER

        private readonly IProductVariantDomain _productProcessor;
        public ProductVariantsController(IProductVariantDomain productProcessor)
        {
            _productProcessor = productProcessor;
        }
        // PRODUCT VARIANTS

        [HttpGet("{idProductVariant}")]
        public async Task<IActionResult> GetProductVariantById(int idProductVariant)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetProductVariantById(idProductVariant)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost]
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

        [HttpPut("{idProductVariant}")]
        public IActionResult UpdateProductVariant(int idProductVariant, [FromBody] ProductVariantDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.UpdateProductVariant(idProductVariant, obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("{idProductVariant}")]
        public IActionResult DeleteProductVariant(int idProductVariant)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.DeleteProductVariant(idProductVariant)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
