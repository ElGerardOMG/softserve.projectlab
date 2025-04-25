using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    public class AssistantController
    {
        private readonly IAssistantDomain _processor;
        public AssistantController(IAssistantDomain processor)
        {
            _processor = processor;
        }

        [HttpGet]
        public IActionResult Ask(string prompt)
        {
            try
            {
                return ResponseHelper.ResponseProcessor(
                    result: _processor.Ask(prompt)
                );
            }
            catch (Exception ex)
            {
                return ResponseHelper.ErrorProcessor(ex.Message, 500);
            }
        }
    }
}
