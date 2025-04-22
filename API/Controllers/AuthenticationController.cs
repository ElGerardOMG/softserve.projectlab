using API.implementations.Domain;
using Microsoft.AspNetCore.Mvc;
using API.Models.Authentication;
namespace API.Controllers
{


    public abstract class AuthenticationController : ControllerBase
    {
        public AuthenticationController()
        {
            
        }

        public abstract Task<IActionResult> Login(LoginDTO auth);

        public abstract Task<IActionResult> SignUp(SignUpDTO auth);
    }
}
