using API.Models;
using API.DTOs;

namespace API.implementations.Interfaces
{
    public interface IProductDomain
    {
        // PRODUCT
        PaginatedResponseDTO<Product> GetAllProducts(bool isActive, FilterDTO filters, string type, int page, int pageSize);
        Task<Product>? GetProductByID(int id);
        bool AddProduct(ProductDTO obj);
        bool UpdateProduct(int id, ProductDTO obj);
        bool DeleteProduct(int id);
    }
}
