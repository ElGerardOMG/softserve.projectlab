using API.Models;

namespace API.implementations.Domain.Interfaces;

public interface IUserPaymentMethodsProcessor
{
    public bool AddMethodToUser(IPayment payment);

    public List<IPayment> GetMethodsFromUser(int id);
    public IPayment GetMethodById(int id);
    public IPayment RemoveMethod(IPayment payment);
    public IPayment RemoveMethod(int id);

    public bool RemoveAllMethodsFromUser(int id);

    public bool UpdateMethod(IPayment payment);


}
