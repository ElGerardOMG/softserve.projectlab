using API.Models.Entities;

namespace API.implementations.Domain.Interfaces;
public interface IUserProcessor
{
    List<User> GetAll();
    User? GetUserByID(int id);
    User? CreateUser(User obj);
    User? UpdateUser(int id, User obj);
    bool DeleteUser(int id);
    User? GetUserByEmail(string email);
}

