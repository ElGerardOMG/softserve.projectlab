using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoriesController : ControllerBase
    {
        // PRODUCT CATEGORY CONTROLLER

        private readonly IProductCategoryDomain _productProcessor;
        public ProductCategoriesController(IProductCategoryDomain productProcessor)
        {
            _productProcessor = productProcessor;
        }

        // PRODUCT CATEGORIES
        [HttpGet]
        public IActionResult GetCategories(string? value, bool isActive)
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

        [HttpGet("{idCategory}")]
        public async Task<IActionResult> GetCategory(int idCategory)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.GetProductCategoryByID(idCategory)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost]
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

        [HttpPut("{idCategory}")]
        public IActionResult UpdateCategory(int idCategory, [FromBody] ProductCategoryDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.UpdateProductCategory(idCategory, obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("{idCategory}")]
        public IActionResult DeleteCategory(int idCategory)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _productProcessor.DeleteProductCategory(idCategory)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
