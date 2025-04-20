using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
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
        public IActionResult GetAllCategories(bool? isActive)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.GetAllCategories(isActive)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAttributeCategoryById(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.GetAttributeCategoryById(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost("Create")]
        public IActionResult AddAttributeCategory([FromBody] AttributeCategoryDTO obj)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.AddAttributeCategory(obj)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateAttributeCategory(int id, [FromBody] AttributeCategoryDTO value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.UpdateAttributeCategory(id, value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteAttributeCategory(int id)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.DeleteAttributeCategory(id)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
