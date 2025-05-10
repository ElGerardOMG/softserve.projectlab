using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AttributeCategoriesController : ControllerBase
    {
        private readonly IAttributeCategoryDomain _attributeCategoryProcessor;
        public AttributeCategoriesController(IAttributeCategoryDomain productProcessor)
        {
            _attributeCategoryProcessor = productProcessor;
        }

        [HttpGet]
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

        [HttpGet("{idAttributeCategory}")]
        public async Task<IActionResult> GetAttributeCategoryById(int idAttributeCategory)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.GetAttributeCategoryById(idAttributeCategory)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPost]
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

        [HttpPut("{idAttributeCategory}")]
        public IActionResult UpdateAttributeCategory(int idAttributeCategory, [FromBody] AttributeCategoryDTO value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.UpdateAttributeCategory(idAttributeCategory, value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("{idAttributeCategory}")]
        public IActionResult DeleteAttributeCategory(int idAttributeCategory)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeCategoryProcessor.DeleteAttributeCategory(idAttributeCategory)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
