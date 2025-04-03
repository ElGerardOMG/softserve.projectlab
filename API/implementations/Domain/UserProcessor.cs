using API.Entity;
using API.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.implementations.Domain;


public class UserProcessor : IUserProcessor
{
    private readonly AppDbContext _db;

    public UserProcessor(AppDbContext db)
    {
        _db = db;
    }

    public virtual User? CreateUser(User usr)
    {
        _db.Add<User>(usr);
        return usr;
    }

    public virtual bool DeleteUser(int id)
    {
        User? foundUser = _db.Find<User>([id]);

        if (foundUser == null) return false;
       
        _db.Remove(foundUser);
        _db.SaveChanges();
        return false;
    }

    public virtual List<User> GetAll(bool? isActive)
    {
        return _db.Users.ToList();    
    }

    public virtual User? GetUserByID(int id)
    {
        User? foundUser = _db.Find<User>([id]);
        return foundUser;
    }

    public virtual User? UpdateUser(int id, Product obj)
    {
        return new User();
    }
}
