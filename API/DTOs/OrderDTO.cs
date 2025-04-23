using API.Models;

namespace API.DTOs
{
    public class OrderDTO
    {
        public double? Subtotal { get; set; }

        public double? Taxes { get; set; }

        public double? Interests { get; set; }

        public double? Total { get; set; }

        public List<PaymentIntervalDTO>? Payments { get; set; }

        public string? ReferralCodeStatus { get; set; }

    }

    public class PaymentIntervalDTO
    {
        public int? PaymentNumber { get; set; }
        public double? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
