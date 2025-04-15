using API.implementations.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Create/{id_product}")]
        public bool RegisterAttribute(int id_product, [FromBody] Models.Attribute value)
        {
            return _attributeProcessor.AddAttribute(id_product, value);
        }

        [HttpPatch("Update/{id_attribute}")]
        public bool UpdateAttribute(int id_attribute, [FromBody] Models.Attribute value)
        {
            return _attributeProcessor.UpdateAttribute(id_attribute, value);
        }

        [HttpDelete("Delete/{id_attribute}")]
        public bool DeleteAttribute(int id_attribute)
        {
            return _attributeProcessor.DeleteAttribute(id_attribute);
        }
    }
}
