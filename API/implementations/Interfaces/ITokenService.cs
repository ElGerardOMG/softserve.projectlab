using API.Models.Entities;
namespace API.implementations.Domain.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
