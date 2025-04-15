using API.Models;

namespace API.DTOs
{
    public class AttributeDTO
    {
        public int? IdProduct { get; set; }

        public int? IdProductVariant { get; set; }

        public int? IdAttributeCategory { get; set; }

        public string? Field { get; set; }

        public string? Value { get; set; }
    }
}
