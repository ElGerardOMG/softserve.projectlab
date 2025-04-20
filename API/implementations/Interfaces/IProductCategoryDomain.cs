using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IProductCategoryDomain
    {
        // PRODUCT CATEGORIES
        ResultDTO GetAllProductCategories(bool? isActive);
        ResultDTO GetProductCategoryByID(int id);
        ResultDTO AddProductCategory(ProductCategoryDTO obj);
        ResultDTO UpdateProductCategory(int id, ProductCategoryDTO obj);
        ResultDTO DeleteProductCategory(int id);
    }
}
