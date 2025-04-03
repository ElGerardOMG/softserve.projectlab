using API.Models;

namespace API.implementations.Domain;
public interface IUserProcessor
{
    List<User> GetAll(bool? isActive);
    User? GetUserByID(int id);
    User? CreateUser(User obj);
    User? UpdateUser(int id, Product obj);
    bool DeleteUser(int id);
}

