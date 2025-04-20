using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IProductVariantDomain
    {
        //PRODUCT VARIANTS
        ResultDTO GetProductVariantById(int id);
        ResultDTO AddProductVariant(ProductVariantDTO obj);
        ResultDTO UpdateProductVariant(int id, ProductVariantDTO obj);
        ResultDTO DeleteProductVariant(int id);
    }
}
