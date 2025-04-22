using API.Data;
using API.Models.Entities;
namespace API.implementations.Domain;


public class UserAddressProcessor : IUserAddressProcessor
{
    private readonly ProjectlabContext _db;

    public UserAddressProcessor(ProjectlabContext db)
    {
        _db = db;
    }

    public bool AddAddressToUser(UserAddress userAddress)
    {
        try
        {
            _db.Add<UserAddress>(userAddress);
            _db.SaveChanges();
            return true;
        } catch (Exception E)
        {
            return false;
        }

    }

    public List<UserAddress> GetAddressesFromUser(int id)
    {
        return (from ua in _db.UserAddresses where ua.IdUser == id select ua).ToList();
    }


    public List<User> GetAll()
    {
        return _db.UsersU.ToList();
    }

    public User? GetUserByID(int id)
    {
        User? foundUser = _db.Find<User>([id]);
        return foundUser;
    }

    public User? UpdateUser(int id, User obj)
    {
        //
        return new User();
    }

    public User? GetUserByEmail(string email)
    {
        return (from u in _db.UsersU where u.Email.Equals(email) select u).FirstOrDefault();
    }
}
