using API.Models.Entities;
using API.Data;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using API.implementations.Domain.Interfaces;

namespace API.implementations.Domain;


public class UserProcessor : IUserProcessor
{
    private readonly ProjectlabContext _db;

    public UserProcessor(ProjectlabContext db)
    {
        _db = db;
    }

    public User? CreateUser(User usr)
    {
        _db.Add<User>(usr);
        _db.SaveChanges();
        return usr;
    }

    public bool DeleteUser(int id)
    {
        User? foundUser = _db.Find<User>([id]);

        if (foundUser == null) return false;
       
        _db.Remove(foundUser);
        _db.SaveChanges();
        return false;
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
