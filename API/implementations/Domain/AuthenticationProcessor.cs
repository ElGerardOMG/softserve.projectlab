using Microsoft.AspNetCore.Identity;
using API.Models.Entities;

namespace API.implementations.Domain
{
    public class AuthenticationProcessor  : IAuthenticationProcessor
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationProcessor( UserManager<User> userManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }


    }
}
