using API.implementations.Domain;
using Microsoft.AspNetCore.Mvc;
using API.Models.Authentication;
using API.Models.Entities;
using Microsoft.AspNetCore.Identity;
namespace API.Controllers
{
    [Route("authenticate")]
    public class DBAuthenticationController : AuthenticationController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public DBAuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public override async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)

        { 
            Console.WriteLine("AAAAAA");
            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState);
                System.Diagnostics.Debug.WriteLine(ModelState);
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                Console.WriteLine("Invalid Credentials");
                System.Diagnostics.Debug.WriteLine("Invalid Credentials");
                return Unauthorized("Invalid Credentials");
                
            }

            // TODO: Maybe locking user account after 3 failed signin attempts ?
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);
            Console.WriteLine("eeeeee");
            if (result.IsLockedOut)
            { 
                Console.WriteLine("This account has been blocked");
                System.Diagnostics.Debug.WriteLine("This account has been blocked");
                return Unauthorized("This account has been blocked");
                
            }

            if (result.Succeeded)
            {
                var token = _tokenService.GenerateToken(user); // <-- Implementa este método
                // Puedes devolver el token y otra info básica del usuario
                Console.WriteLine("Ok....");
                System.Diagnostics.Debug.WriteLine("Ok....");
                return Ok(token);           
            }
            Console.WriteLine("Invalid credentials....");
            System.Diagnostics.Debug.WriteLine("Invalid credentials....");
            return Unauthorized("Invalid credentials.");
            
        }

        [HttpPost("signup")]
        public async override Task<IActionResult> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            User user = new User();
            user.UserName = signUpDTO.Username;
            user.Email = signUpDTO.Email;

            IdentityResult result = await _userManager.CreateAsync(user, signUpDTO.Password);
            if( result.Succeeded)
            {
                return Ok("User registered successfully");
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(errors);
        }

        // TODO: Maybe authenticate emails for recently registered users, but we will be using and
        // email service and stuff

        //
        /*
        [HttpPost("Authenticate")]
        public virtual IActionResult AuthenticateEmail(string email)
        {
            // Authenticate somehow...
            User? u = _UserProcessor.GetUserByEmail(email);
            if (u == null) return BadRequest("Error");

            u.IsActive = true;

            return Redirect("login");
        }
        */
    }
}
