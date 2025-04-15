using API.Models;

namespace API.DTOs
{
    public class ProductDTO
    {
        public string? Name { get; set; }

        public string? ProductType { get; set; }

        public int? ProductCategory { get; set; }

        public string? Brand { get; set; }

        public string? Family { get; set; }

        public ICollection<ProductVariantDTO>? ProductVariants { get; set; }
    }
}
