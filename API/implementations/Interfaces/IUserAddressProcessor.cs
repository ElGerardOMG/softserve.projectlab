using API.Data;
using API.Models.Entities;

namespace API.implementations.Interfaces;


public interface IUserAddressProcessor
{

    public bool AddAddressToUser(UserAddress userAddress);
    public List<UserAddress> GetAddressesFromUser(int id);
    public UserAddress RemoveAddress(UserAddress userAddress);
    public UserAddress RemoveAddress(int id);
    public bool RemoveAllAddress(int id);
    public bool UpdateAddress(UserAddress userAddress);

    }
