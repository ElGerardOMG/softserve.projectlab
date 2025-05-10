namespace API.DTOs
{
    public class ShipmentDTO
    {
        public DateTime? EstimatedArrival { get; set; }
        public DateTime? Arrival { get; set; }
    }
    public class CreateShipmentDTO
    {
        public int IdOrder { get; set; }
        public int IdUser { get; set; }
        public string ShipmentCompany { get; set; }
        public string GuideNumber { get; set; }
        public DateTime ShipmentDate { get; set; }
        public DateTime EstimatedArrival { get; set; }
    }
}
