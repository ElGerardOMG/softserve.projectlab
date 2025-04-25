using API.Data;
using API.implementations.Domain.Interfaces;
using API.Models;


namespace API.implementations.Domain
{
    public class UserPaypalProcessor : IUserPaymentMethodsProcessor
    {
        private readonly ProjectlabContext _db;
        public UserPaypalProcessor(ProjectlabContext db)
        {
            _db = db;
        }

        public bool AddMethodToUser(IPayment payment)
        {
            try
            {
                _db.Add<UserPaypalProcessor>( (UserPaypalProcessor) payment);
                _db.SaveChanges();
                return true;
            }
            catch (Exception E)
            {
                return false;
            }

        }

        public List<IPayment> GetMethodsFromUser(int id)
        {
            return (from ua in _db.UserPaypalPayments where ua.UserId == id select ((IPayment) ua)).ToList();
        }

        public IPayment GetMethodById(int id)
        {
            return (from ua in _db.UserPaypalPayments where ua.Id == id select (IPayment) ua).FirstOrDefault();
        }


        public IPayment RemoveMethod(IPayment payment)
        {
            _db.Remove((UserPaypalPayment) payment);
            _db.SaveChanges();
            return (UserPaypalPayment) payment;
        }
        public IPayment RemoveMethod(int id)
        {
            UserPaypalPayment? payment = (from ua in _db.UserPaypalPayments where ua.Id == id select ua).FirstOrDefault();

            _db.Remove(payment);
            _db.SaveChanges();
            return payment;
        }

        public bool RemoveAllMethodsFromUser(int id) 
        {
            try
            {
                _db.RemoveRange((from ua in _db.UserPaypalPayments where ua.UserId == id select ua).ToList());
                _db.SaveChanges();
            }
            catch (Exception E)
            {
                return false;
            }

            return true;

        }

        public bool UpdateMethod(IPayment payment)
        {
            try
            {
                var entity = _db.Update<UserPaypalPayment>((UserPaypalPayment) payment);
                _db.SaveChanges();
                return true;
            }
            catch (Exception E)
            {
                return false;
            }
        }

    }

    
    
}
