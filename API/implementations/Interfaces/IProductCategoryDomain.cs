using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IProductCategoryDomain
    {
        // PRODUCT CATEGORIES
        List<ProductCategory> GetAllProductCategories(bool? isActive);
        ProductCategory? GetProductCategoryByID(int id);
        bool AddProductCategory(ProductCategoryDTO obj);
        bool UpdateProductCategory(int id, ProductCategoryDTO obj);
        bool DeleteProductCategory(int id);
    }
}
