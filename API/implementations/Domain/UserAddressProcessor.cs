using API.Data;
using API.implementations.Interfaces;
using API.Models;
namespace API.implementations.Domain;


public class UserAddressProcessor : IUserAddressProcessor
{
    private readonly ProjectlabContext _db;

    public UserAddressProcessor(ProjectlabContext db)
    {
        _db = db;
    }

    /**
     * Inserta un registro de userAddress en la tabla
     */
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

    /**
     * Devuelve la lista de direcciones de usuario dado un id de un usuario
     */
    public List<UserAddress> GetAddressesFromUser(int id)
    {
        return (from ua in _db.UserAddresses where ua.IdUser == id select ua).ToList();
    }


    public UserAddress RemoveAddress(UserAddress userAddress)
    {
        _db.Remove(userAddress);
        _db.SaveChanges();
        return userAddress;
    }
    public UserAddress RemoveAddress(int id)
    {
        UserAddress? userAddress = (from ua in _db.UserAddresses where ua.Id == id select ua).FirstOrDefault();

        _db.Remove(userAddress);
        _db.SaveChanges();
        return userAddress;
    }

    public bool RemoveAllAddress(int id)
    {
        try
        {
            _db.RemoveRange((from ua in _db.UserAddresses where ua.IdUser == id select ua).ToList());
            _db.SaveChanges();
        } catch (Exception E)
        {
            return false;
        }

        return true;
        
    }

    public bool UpdateAddress(UserAddress userAddress)
    {
        try
        {
            var entity = _db.Update(userAddress);
            _db.SaveChanges();
            return true;
        } catch (Exception E)
        {
            return false;
        }
    }

}
