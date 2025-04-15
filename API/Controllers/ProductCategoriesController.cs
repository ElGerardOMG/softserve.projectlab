using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
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
        public List<ProductCategory> GetCategories([FromBody] string? value, bool isActive)
        {
            return _productProcessor.GetAllProductCategories(isActive);
        }

        [HttpGet("GetById/{id}")]
        public ProductCategory? GetCategory(int id)
        {
            return _productProcessor.GetProductCategoryByID(id);
        }

        [HttpPost("Create")]
        public bool AddCategory([FromBody] ProductCategoryDTO obj)
        {
            return _productProcessor.AddProductCategory(obj);
        }

        [HttpPut("Update/{id}")]
        public bool UpdateCategory(int id, [FromBody] ProductCategoryDTO obj)
        {
            return _productProcessor.UpdateProductCategory(id, obj);
        }

        [HttpDelete("Delete/{id}")]
        public bool DeleteCategory(int id)
        {
            return _productProcessor.DeleteProductCategory(id);
        }
    }
}
