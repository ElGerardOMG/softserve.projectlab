using API.Data;
using API.implementations.Domain.Interfaces;
using API.Models;


namespace API.implementations.Domain
{
    public class UserCardProcessor : IUserPaymentMethodsProcessor
    {
        private readonly ProjectlabContext _db;
        public UserCardProcessor(ProjectlabContext db)
        {
            _db = db;
        }

        public bool AddMethodToUser(IPayment payment)
        {
            try
            {
                _db.Add<UserCardPayment>( (UserCardPayment) payment);
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

            return (from ua in _db.UserCardPayments where ua.UserId == id select ( (IPayment) ua )).ToList();

        }

        public IPayment GetMethodById(int id)
        {
            return (from ua in _db.UserCardPayments where ua.Id == id select (IPayment)ua).FirstOrDefault();
        }


        public IPayment RemoveMethod(IPayment payment)
        {
            _db.Remove((UserCardPayment) payment);
            _db.SaveChanges();
            return (UserCardPayment) payment;
        }
        public IPayment RemoveMethod(int id)
        {
            try
            {
                UserCardPayment? payment = (from ua in _db.UserCardPayments where ua.Id == id select ua).FirstOrDefault();
                _db.Remove(payment);
                _db.SaveChanges();
                return payment;
            }
            catch (Exception E)
            {
                Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
                return null;
            }

        }

        public bool RemoveAllMethodsFromUser(int id) 
        {
            try
            {
                _db.RemoveRange((from ua in _db.UserCardPayments where ua.UserId == id select ua).ToList());
                _db.SaveChanges();
            }
            catch (Exception E)
            {
                Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
                return false;
            }

            return true;

        }

        public bool UpdateMethod(IPayment payment)
        {
            try
            {
                var entity = _db.Update<UserCardPayment>((UserCardPayment) payment);
                _db.SaveChanges();
                return true;
            }
            catch (Exception E)
            {
                Console.WriteLine($"{E.Message}\n...\n{E.StackTrace}");
                return false;
            }
        }

    }

    
    
}
