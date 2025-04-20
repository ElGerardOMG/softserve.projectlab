using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController
    {
        // PRODUCT CATEGORY CONTROLLER

        private readonly IProductCategoryDomain _productProcessor;
        public ProductCategoriesController(IProductCategoryDomain productProcessor)
        {
            _productProcessor = productProcessor;
        }

        // PRODUCT CATEGORIES

        [HttpPost("GetFiltered/{isActive}")]
        public IActionResult GetCategories([FromBody] string? value, bool isActive)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetAllProductCategories(isActive)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetProductCategoryByID(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("Create")]
        public IActionResult AddCategory([FromBody] ProductCategoryDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.AddProductCategory(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] ProductCategoryDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.UpdateProductCategory(id, obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.DeleteProductCategory(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
