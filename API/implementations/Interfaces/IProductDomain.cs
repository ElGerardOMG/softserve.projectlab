using API.Models;
using API.DTOs;

namespace API.implementations.Interfaces
{
    public interface IProductDomain
    {
        // PRODUCT
        ResultDTO GetAllProducts(bool isActive, FilterDTO filters, string type, int page, int pageSize);
        ResultDTO GetProductByID(int id);
        ResultDTO AddProduct(ProductDTO obj);
        ResultDTO UpdateProduct(int id, ProductDTO obj);
        ResultDTO DeleteProduct(int id);
    }
}
