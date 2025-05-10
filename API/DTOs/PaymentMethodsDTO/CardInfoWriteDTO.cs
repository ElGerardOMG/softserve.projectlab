namespace API.DTOs.PaymentMethodsDTO
{
    public class CardInfoWriteDTO 
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public int? CardExpirationYear { get; set; }
        public int? CardExpirationMonth { get; set; }
    }
}
