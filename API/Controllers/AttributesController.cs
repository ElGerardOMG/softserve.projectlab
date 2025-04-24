using API.implementations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTOs;
using API.Utils.Implementations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AttributesController : ControllerBase  
    {
        // PRODUCT ATTRIBUTE CONTROLLER

        private readonly IAttributeDomain _attributeProcessor;
        public AttributesController(IAttributeDomain attributeProcessor)
        {
            _attributeProcessor = attributeProcessor;
        }

        [HttpPost]
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

        [HttpPatch("{idAttribute}")]
        public IActionResult UpdateAttribute(int idAttribute, string field, string value)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeProcessor.UpdateAttribute(idAttribute, field, value)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }

        [HttpDelete("{idAttribute}")]
        public IActionResult DeleteAttribute(int idAttribute)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _attributeProcessor.DeleteAttribute(idAttribute)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
