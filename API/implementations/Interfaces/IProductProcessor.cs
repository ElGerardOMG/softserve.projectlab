using API.Models;

namespace API.implementations.Domain.Interfaces
{
    public interface IProductProcessor
    {
        List<Product> GetAllProducts(bool? isActive);
        Product? GetProductByID(int id);
        bool AddProduct(string type, Product obj);
        bool UpdateProduct(int id, Product obj);
        bool DeleteProduct(int id);
        bool AddAttribute(int id_product, Models.Entities.Attribute obj);
        bool UpdateAttribute(int id_attribute, Models.Entities.Attribute obj);
        bool DeleteAttribute(int id_attribute);
    }
}
