using API.DTOs;

namespace API.implementations.Interfaces
{
    public interface IOrderDomain
    {
        public ResultDTO CreateOrder(CreateOrderDTO obj);
        public ResultDTO GetOrder(int id_order);
        public ResultDTO CancelOrder(int id_order);
        public ResultDTO GetOrderStatus(int id_order);
        public ResultDTO UpdateOrderStatus(int id_order, string status);
        public ResultDTO GetOrdersByUser(int id_user, FilterDTO filters);
    }
}
