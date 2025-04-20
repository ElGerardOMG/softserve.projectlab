using API.implementations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTOs;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase  
    {
        // PRODUCT ATTRIBUTE CONTROLLER

        private readonly IAttributeDomain _attributeProcessor;
        public AttributesController(IAttributeDomain attributeProcessor)
        {
            _attributeProcessor = attributeProcessor;
        }

        [HttpPost("Create")]
        public IActionResult RegisterAttribute([FromBody] AttributeDTO value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeProcessor.AddAttribute(value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpPatch("Update/{id_attribute}")]
        public IActionResult UpdateAttribute(int id_attribute, string field, string value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeProcessor.UpdateAttribute(id_attribute, field, value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("Delete/{id_attribute}")]
        public IActionResult DeleteAttribute(int id_attribute)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeProcessor.DeleteAttribute(id_attribute)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
