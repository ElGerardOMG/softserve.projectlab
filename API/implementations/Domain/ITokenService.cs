using API.Models.Entities;
namespace API.implementations.Domain
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
