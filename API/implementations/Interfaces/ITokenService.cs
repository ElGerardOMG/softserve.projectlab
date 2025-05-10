using API.Models;
namespace API.implementations.Domain.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
