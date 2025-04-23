using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace API.implementations.Domain
{
    public class OrderDomain : IOrderDomain
    {
        private readonly ProjectlabContext _db;
        public OrderDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ResultDTO CreateOrder(CreateOrderDTO obj)
        {
            string message = "Order created";
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
                    description = "Invalid order data"
                };
            }
            // GET CART
            Cart cart = _db.Cart.FirstOrDefault(c => c.Id == obj.id_cart);
            // GET CART LIST OF PRODUCTS
            List<CartDetail> cartDetails = _db.CartDetail.Where(c => c.IdCart == obj.id_cart).ToList();
            // GET THE SUBTOTAL SUM OF THE PRODUCTS AND ITS DISCOUNTS
            if (cartDetails != null)
            {
                foreach (var item in cartDetails)
                {
                    double? productPrice = _db.ProductVariants
                        .Where(p => p.Id == item.IdProductVariant).First().Price;
                    subtotal += productPrice * item.Quantity;
                }
            }
            // GET THE TAXES OF THE TOTAL

            // GET THE TOTAL
            taxes = Taxes.GetTaxes() * subtotal;
            total = subtotal + taxes;

            // CALCULATE THE FINANCED PAYMENTS IF ENABLED
            List<PaymentIntervalDTO> paymentIntervals = new();
            if (obj.is_financed)
            {
                double? tempInterval = (total / obj.payment_count) + ((total / obj.payment_count) * Taxes.GetIntervalInterests());
                for (var i = 0; i <= obj.payment_count - 1; i++)
                {
                    interests += ((total / obj.payment_count) * Taxes.GetIntervalInterests());
                    paymentIntervals.Add(new PaymentIntervalDTO
                    {
                        PaymentNumber = i + 1,
                        PaymentAmount = tempInterval,
                        PaymentDate = DateTime.Now.AddMonths(i)
                    });
                }
            }

            total += interests;

            // GET IF REFERAL CODE EXISTS
            if (obj.referal_code != null)
            {
                var refUser = _db.Users.FirstOrDefault(c => c.ReferralCode == obj.referal_code);
                if (refUser == null)
                {
                    referralCodeStatus = "NOT_FOUND";
                }
                else
                {
                    referalCodeDiscount = Taxes.GetReferalCodeDiscount();
                    referralCodeUserId = refUser.Id;
                    total -= referalCodeDiscount;
                }
            }
            else
            {
                referralCodeUserId = null;
            }

            // CHECK IF PREVIEW OR CREATE ORDER
            if (obj.preview == true)
            {
                OrderDTO order = new OrderDTO
                {
                    Subtotal = subtotal,
                    Total = total,
                    Taxes = taxes,
                    Interests = interests,
                    Payments = paymentIntervals,
                    ReferralCodeStatus = referralCodeStatus
                };
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "Order preview",
                    data = order
                };
            }
            else
            {
                Order newOrder =
                    new Order
                    {
                        IdUser = cart.IdUser,
                        Subtotal = subtotal,
                        Taxes = taxes,
                        Total = total,
                        IdUserAddress = obj.id_user_address,
                        PaymentType = obj.payment_type,
                        IdUserPaymentCard = obj.id_user_payment_card,
                        IdUserPaymentPaypal = obj.id_user_payment_paypal,
                        IsFinanced = obj.is_financed,
                        ReferralUser = referralCodeUserId,
                        CreatedAt = DateTime.Now,
                        UpdateAt = null,
                        DeletedAt = null,
                        IsActive = true,
                        Status = "Pending"
                    };
                _db.Orders.Add(newOrder);
                _db.SaveChanges();
                foreach (var item in cartDetails)
                {
                    OrderDetail newOrderDetail =
                        new OrderDetail
                        {
                            IdOrder = newOrder.Id,
                            IdProductVariant = item.IdProductVariant,
                            Quantity = item.Quantity,
                            CreatedAt = DateTime.Now,
                            // IMPLEMENT DISCOUNTS
                            UpdateAt = null,
                            DeletedAt = null,
                            IsActive = true
                        };
                    _db.OrderDetails.Add(newOrderDetail);
                }
                if (obj.is_financed)
                {
                    // FURTHER DEVELOPMENT
                }
                _db.SaveChanges();

                return new ResultDTO
                {
                    statusCode = 201,
                    description = "Order created",
                    data = new { orderId = newOrder.Id }
                };
            }
        }
        public ResultDTO GetOrder(int id_order)
        {
            Order order = _db.Orders.FirstOrDefault(c => c.Id == id_order);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Order not found",
                };
            }
            OrderDetail orderDetail = _db.OrderDetails.FirstOrDefault(c => c.IdOrder == id_order);

            return new ResultDTO
            {
                statusCode = 200,
                description = "Order detail",
                data = new
                {
                    order = order,
                    orderDetail = orderDetail
                }
            };
        }
        public ResultDTO CancelOrder(int id_order)
        {
            Order order = _db.Orders.FirstOrDefault(c => c.Id == id_order);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Order not found",
                };
            }
            order.Status = "Canceled";
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Order canceled",
                data = null
            };

        }
        public ResultDTO GetOrderStatus(int id_order)
        {
            Order order = _db.Orders.FirstOrDefault(c => c.Id == id_order);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Order not found",
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Order status",
                data = order.Status
            };
        }
        public ResultDTO UpdateOrderStatus(int id_order, string status)
        {
            Order order = _db.Orders.FirstOrDefault(c => c.Id == id_order);
            if (order == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Order not found",
                };
            }
            order.Status = status;
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Order status updated",
                data = null
            };
        }
        public ResultDTO GetOrdersByUser(int id_user)
        {
            List<Order> orders = _db.Orders
                .Where(c => c.IdUser == id_user)
                .ToList();
            if (orders == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Orders not found",
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Orders by user",
                data = orders
            };
        }
    }
}
