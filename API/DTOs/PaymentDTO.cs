namespace API.DTOs
{
    public class PaymentDTO
    {
        public decimal amount { get; set; }
        public string currency { get; set; } = null!;
        public string? method { get; set; }
        public CreditCardDTO? credit_card { get; set; }
        public PaypalDTO? paypal { get; set; }

    }

    public class CreditCardDTO
    {
        public string card_number { get; set; } = null!;
        public int expiry_month { get; set; }
        public int expiry_year { get; set; }
        public string cvv { get; set; } = null!;
    }

    public class PaypalDTO
    {
        public string email { get; set; } = null!;
    }
    public class TransactionDTO
    {
        public string transaction_id { get; set; }
        public string? status { get; set; }
        public object? message { get; set; }
    }
}
