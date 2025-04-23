using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models.Entities;
using API.Models.Configuration;
using Microsoft.Extensions.Options;
using API.implementations.Domain.Interfaces;
using API.Utils.Implementations;

namespace API.implementations.Domain
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings; 

        public TokenService(IOptions<JwtSettings> jwtSettingsOptions)
        {
            _jwtSettings = jwtSettingsOptions.Value; 
        }

        public string GenerateToken(User user)
        {
            /*
            var claims = new List<Claim>()
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),

                };
            */
            var claims = UserClaimUtils.CreateIdentity(user);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims, //new ClaimsIdentity(claims), 
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = creds,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
