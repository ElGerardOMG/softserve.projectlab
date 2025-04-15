using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeCategoriesController : ControllerBase
    {
        private readonly IAttributeCategoryDomain _attributeCategoryProcessor;
        public AttributeCategoriesController(IAttributeCategoryDomain productProcessor)
        {
            _attributeCategoryProcessor = productProcessor;
        }

        [HttpPost("GetFiltered/{isActive}")]
        public List<AttributeCategory> GetAllCategories(bool? isActive)
        {
            return _attributeCategoryProcessor.GetAllCategories(isActive);
        }

        [HttpGet("GetById/{id}")]
        public async Task<AttributeCategory>? GetAttributeCategoryById(int id)
        {
            return await _attributeCategoryProcessor.GetAttributeCategoryById(id);
        }

        [HttpPost("Create")]
        public bool AddAttributeCategory([FromBody] AttributeCategoryDTO obj)
        {
            return _attributeCategoryProcessor.AddAttributeCategory(obj);
        }

        [HttpPut("Update/{id}")]
        public bool UpdateAttributeCategory(int id, [FromBody] AttributeCategoryDTO value)
        {
            return _attributeCategoryProcessor.UpdateAttributeCategory(id, value);
        }

        [HttpDelete("Delete/{id}")]
        public bool DeleteAttributeCategory(int id)
        {
            return _attributeCategoryProcessor.DeleteAttributeCategory(id);
        }
    }
}
