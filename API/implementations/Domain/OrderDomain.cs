using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            if (cart.IsActive == false)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Cart already purchased",
                };
            }
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
                    // GET THE DISCOUNTS OF THE PRODUCT
                    // LATER
                    // GET PRODUCT AVAILABILITY
                    ProductVariant pv = _db.ProductVariants.FirstOrDefault(c => c.Id == item.IdProductVariant);
                    if (pv.Stock < item.Quantity)
                    {
                        return new ResultDTO
                        {
                            statusCode = 500,
                            description = $"Only {pv.Stock} products remaining",
                            data = new { idProductVariant = item.IdProductVariant, stock = pv.Stock }
                        };
                    }
                    if (pv.Stock == 0)
                    {
                        return new ResultDTO
                        {
                            statusCode = 500,
                            description = "Product out of stock",
                            data = null
                        };
                    }
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
                // PROCESS PAYMENT
                const string url = "http://159.54.158.214:8000/pay"; // FAKE BANK API
                PaymentDTO payData = new PaymentDTO()
                {
                    amount = (decimal)total,
                    currency = "USD",
                    method = obj.payment_type,
                    credit_card = new CreditCardDTO
                    {
                        card_number = "1111222233334444", //PASSTHROUG TO VALIDATE THE API 16 CHARACTERES REQUIREMENT
                        expiry_month = DateTime.Now.Month + 1,
                        expiry_year = DateTime.Now.Year + 1,
                        cvv = "111"
                    },
                    paypal = new PaypalDTO
                    {
                        email = "example@example.example" //PASSTHROGH TO VALIDATE THE API @ REQUIREMENT
                    }
                };
                if (obj.payment_type == "credit_card")
                {
                    UserCardPayment userCardPayment = _db.UserCardPayments.FirstOrDefault(c => c.Id == obj.id_user_payment_card);

                    payData.credit_card = new CreditCardDTO 
                    {
                        card_number = userCardPayment.CardNumber,
                        expiry_month = userCardPayment.CardExpirationMonth.Value,
                        expiry_year = userCardPayment.CardExpirationYear.Value,
                        cvv = obj.cvv.ToString(),
                    };
                }
                if(obj.payment_type == "paypal")
                {
                    UserPaypalPayment userPaypalPayment = _db.UserPaypalPayments.FirstOrDefault(c => c.Id == obj.id_user_payment_paypal);
                    payData.paypal = new PaypalDTO
                    {
                        email = userPaypalPayment.Email,
                    };
                }
                //HERE WE CAN IMPLEMENT ANY KIND OF PAYMENT TYPES

                TransactionDTO response = APIHelper.PostApiData(url, payData);
                if(response.status != "success")
                {
                    return new ResultDTO
                    {
                        statusCode = 500,
                        description = "Payment error",
                        data = new { detail = response.message }
                    };
                }

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
                cart.IsActive = false;
                //Close the actual cart for the order and let it stored hidden
                _db.Cart.Update(cart);
                Cart newCart = new Cart
                {
                    IdUser = cart.IdUser,
                    CreatedAt = DateTime.Now,
                    UpdateAt = null,
                    IsActive = true
                };
                //Create a new cart for the user
                _db.Cart.Add(newCart);
                //Register the roder
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
                    ProductVariant pv = _db.ProductVariants.FirstOrDefault(c => c.Id == item.IdProductVariant);
                    pv.Stock -= item.Quantity;
                    _db.ProductVariants.Update(pv);
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
                    data = new { orderId = newOrder.Id, transactionDetail = response }
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
            if(order.Status == status)
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null
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
        public ResultDTO GetOrdersByUser(int id_user, FilterDTO filter)
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
