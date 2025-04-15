using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;

namespace API.implementations.Domain
{
    public class ProductCategoryDomain : IProductCategoryDomain
    {
        private readonly ProjectlabContext _db;
        public ProductCategoryDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        // PRODUCT CATEGORIES

        public List<ProductCategory> GetAllProductCategories(bool? isActive)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            productCategories = _db.ProductCategories.ToList();
            return productCategories;
        }

        public ProductCategory? GetProductCategoryByID(int id)
        {
            return _db.ProductCategories.FirstOrDefault(p => p.Id == id);
        }

        public bool AddProductCategory(ProductCategoryDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _db.ProductCategories.Add(DtoMapper.Mapper<ProductCategoryDTO, ProductCategory>(obj));
            _db.SaveChanges();
            return true;
        }

        public bool UpdateProductCategory(int id, ProductCategoryDTO obj)
        {
            var existingProductCategory = _db.ProductCategories.FirstOrDefault(p => p.Id == id);
            if (existingProductCategory == null)
            {
                return false;
            }
            existingProductCategory.Name = obj.Name;
            _db.ProductCategories.Update(existingProductCategory);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteProductCategory(int id)
        {
            var productCategory = _db.ProductCategories.FirstOrDefault(p => p.Id == id);
            if (productCategory == null)
            {
                return false;
            }
            _db.ProductCategories.Remove(productCategory);
            _db.SaveChanges();
            return true;
        }
    }
}
