using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IProductVariantDomain
    {
        //PRODUCT VARIANTS
        ProductVariant? GetProductVariantById(int id);
        bool AddProductVariant(ProductVariantDTO obj);
        bool UpdateProductVariant(int id, ProductVariantDTO obj);
        bool DeleteProductVariant(int id);
    }
}
