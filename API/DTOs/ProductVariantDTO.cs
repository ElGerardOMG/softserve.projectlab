using API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class ProductVariantDTO
    {
        [NotMapped]
        public int? IdProduct { get; set; }

        public string? SubName { get; set; }

        public string? Sku { get; set; }

        public string? SubFamily { get; set; }

        public double? Price { get; set; }

        public int? Stock { get; set; }

        public ICollection<AttributeDTO>? ProductAttributes { get; set; }
    }
}
