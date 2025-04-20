using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Utils.Implementations
{
    public class ResponseHelper
    {
        public static IActionResult ResponseProcessor(ResultDTO? result)
        {
            ResponseDTO response = new() { description = result.description, data = result.data, error = result.error };
            return new ObjectResult(response) { StatusCode = result.statusCode }; ;
        }

        public static IActionResult ErrorProcessor(string error, int statusCode=500)
        {
            ResponseDTO response = new() { error = error };
            return new ObjectResult(response) { StatusCode = statusCode }; ;
        }
    }
}
