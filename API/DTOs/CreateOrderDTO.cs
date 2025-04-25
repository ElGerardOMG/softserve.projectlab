namespace API.DTOs
{
    public class CreateOrderDTO
    {
        public int id_cart { get; set; }
        public string? referal_code { get; set; }
        public bool is_financed { get; set; }
        public int payment_count { get; set; }
        public bool? preview { get; set; }
        public string payment_type { get; set; }
        public int id_user_address { get; set; }
        public int id_user_payment_card { get; set; }
        public int cvv { get; set; }
        public int id_user_payment_paypal { get; set; }

    }
}
