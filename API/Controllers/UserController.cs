using API.implementations.Domain;
using API.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserProcessor _UserProcessor;

        public UserController(IUserProcessor userProcessor)
        {
            _UserProcessor = userProcessor;
        }


        //  TODO: No devolver el user completo. Crear un DTO igual al user en donde no se
        //  incluyan contraseñas, métodos de pago etc.

        [HttpGet("{user_id}")]
        public virtual IActionResult GetUser(int user_id)
        {
            return Ok(_UserProcessor.GetUserByID(user_id));
        }

        [HttpGet]
        public virtual JsonResult GetAll()
        {

            return new JsonResult(_UserProcessor.GetAll());
        }


    }
}
