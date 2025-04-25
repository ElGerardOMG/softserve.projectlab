using API.Models.Entities;
namespace API.implementations.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
