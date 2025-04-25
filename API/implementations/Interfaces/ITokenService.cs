using API.Models;
namespace API.implementations.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
