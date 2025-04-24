using API.DTOs;

namespace API.implementations.Interfaces
{
    public interface IShipmentsDomain
    {
        public ResultDTO CreateDelivery(CreateShipmentDTO obj);
        public ResultDTO GetDelivery(int id_delivery);
        public ResultDTO GetDeliveriesByUser(int id_user, FilterDTO? filters);
        public ResultDTO UpdateDelivery(ShipmentDTO obj);
        public ResultDTO UpdateDeliveryStatus(int id_delivery, string status);
        public ResultDTO DeleteDelivery(int id_delivery);

    }
}
