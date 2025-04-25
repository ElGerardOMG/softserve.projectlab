using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Utils.Implementations;
using API.Models;

namespace API.implementations.Domain
{
    public class ShipmentsDomain : IShipmentsDomain
    {
        private readonly ProjectlabContext _db;
        public ShipmentsDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public ResultDTO CreateDelivery(CreateShipmentDTO obj)
        {
            string message = "Delivery created";
            string referralCodeStatus = "OK";
            int? referralCodeUserId = 0;
            double? total = 0;
            double? subtotal = 0;
            double? taxes = 0;
            double? interests = 0;
            double? referalCodeDiscount = 0;
            // CHECK IF OBJECT IS VALID
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid delivery data"
                };
            }
            // GET ORDER
            Order order = _db.Orders.FirstOrDefault(o => o.Id == obj.IdOrder);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Order not found"
                };
            }
            // GET USER
            User user = _db.Users.FirstOrDefault(u => u.Id == order.IdUser);
            if (user == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "User not found"
                };
            }
            // CHECK IF DELIVERY ALREADY EXISTS
            Shipment existingDelivery = _db.Shipments.FirstOrDefault(d => d.IdOrder == order.Id && d.IsActive == true);
            if (existingDelivery != null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Delivery already exists"
                };
            }
            // CREATE SHIPMENT
            Shipment delivery = new Shipment
            {
                IdOrder = obj.IdOrder,
                IdUser = obj.IdUser,
                ShipmentCompany = obj.ShipmentCompany,
                GuideNumber = obj.GuideNumber,
                ShipmentDate = obj.ShipmentDate,
                EstimatedArrival = obj.EstimatedArrival,
                Arrival = null,
                CreatedAt = DateTime.Now,
                UpdateAt = null,
                DeletedAt = null,
                IsActive = true
            };
            order.Status = "Sent";
            order.UpdateAt = DateTime.Now;
            _db.Update(order);
            _db.Add(delivery);
            _db.SaveChanges();

            return new ResultDTO
            {
                statusCode = 201,
                description = "Delivery succesfully created",
                data = null
            };


        }

        public ResultDTO GetDelivery(int id_delivery)
        {
            // CHECK IF OBJECT IS VALID
            if (id_delivery <= 0)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid delivery data"
                };
            }
            // GET DELIVERY
            Shipment delivery = _db.Shipments.FirstOrDefault(d => d.Id == id_delivery);
            if (delivery == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Delivery not found"
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Delivery found",
                data = delivery
            };
        }
        public ResultDTO GetDeliveriesByUser(int id_user, FilterDTO? filters)
        {
            // CHECK IF OBJECT IS VALID
            if (id_user <= 0)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid user data"
                };
            }
            // GET DELIVERIES
            List<Shipment> deliveries = _db.Shipments.Where(d => d.IdUser == id_user).ToList();
            if (deliveries == null || deliveries.Count == 0)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Deliveries not found"
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Deliveries found",
                data = deliveries
            };
        }
        public ResultDTO UpdateDelivery(int id, ShipmentDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid delivery data"
                };
            }
            // GET DELIVERY
            Shipment delivery = _db.Shipments.FirstOrDefault(d => d.Id == id);
            if (delivery == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Delivery not found"
                };
            }
            DateTime ObjEstimatedArrival = DateTime.Now;
            DateTime ObjArrival = DateTime.Now;
            if (obj.EstimatedArrival != null) ObjEstimatedArrival = obj.EstimatedArrival.Value;
            if (obj.Arrival != null) ObjArrival = obj.Arrival.Value;

            DateTime DbEstimatedArrival = DateTime.Now;
            DateTime DbArrival = DateTime.Now;
            if (delivery.EstimatedArrival != null) DbEstimatedArrival = delivery.EstimatedArrival.Value;
            if (delivery.Arrival != null) DbArrival = delivery.Arrival.Value;
            // UPDATE DELIVERY
            if (DateTime.Compare(DbEstimatedArrival, ObjEstimatedArrival) == 0 && DateTime.Compare(DbArrival, ObjArrival) == 0)
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null,
                    error = null
                };
            }
            if(obj.EstimatedArrival != null) delivery.EstimatedArrival = obj.EstimatedArrival;
            if(obj.Arrival != null)delivery.Arrival = obj.Arrival;
            delivery.UpdateAt = DateTime.Now;
            _db.Shipments.Update(delivery);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Delivery updated",
                data = null
            };
        }
        public ResultDTO UpdateDeliveryStatus(int id_delivery, string status)
        {
            // CHECK IF OBJECT IS VALID
            if (id_delivery <= 0)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid delivery data"
                };
            }
            // GET DELIVERY
            Shipment delivery = _db.Shipments.FirstOrDefault(d => d.Id == id_delivery);
            if (delivery == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Delivery not found"
                };
            }
            // GET ORDER
            Order order = _db.Orders.FirstOrDefault(o => o.Id == delivery.IdOrder);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Order not found"
                };
            }
            // UPDATE DELIVERY STATUS
            order.Status = status;
            delivery.UpdateAt = DateTime.Now;
            order.UpdateAt = DateTime.Now;
            _db.Shipments.Update(delivery);
            _db.Orders.Update(order);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Delivery status updated",
                data = delivery
            };
        }
        public ResultDTO DeleteDelivery(int id_delivery)
        {
            // CHECK IF OBJECT IS VALID
            if (id_delivery <= 0)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid delivery data"
                };
            }
            // GET DELIVERY
            Shipment delivery = _db.Shipments.FirstOrDefault(d => d.Id == id_delivery);
            if (delivery == null)
            {
                return new ResultDTO
                {
                    statusCode = 404,
                    description = "Delivery not found"
                };
            }
            // DELETE DELIVERY
            delivery.IsActive = false;
            delivery.DeletedAt = DateTime.Now;
            _db.Shipments.Update(delivery);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Delivery deleted"
            };
        }
    }
}
