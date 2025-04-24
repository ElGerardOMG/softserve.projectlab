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

        public ResultDTO GetAllProductCategories(bool? isActive)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            productCategories = _db.ProductCategories.ToList();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product category list",
                data = productCategories,
                error = null
            };
        }

        public ResultDTO GetProductCategoryByID(int id)
        {
            ProductCategory productCategory = _db.ProductCategories.FirstOrDefault(p => p.Id == id);
            if (productCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product category not found",
                    data = null,
                    error = null
                };
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product category detail",
                data = productCategory,
                error = null
            };
        }

        public ResultDTO AddProductCategory(ProductCategoryDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid product category object",
                    data = null,
                    error = null
                };
            }
            _db.ProductCategories.Add(DtoMapper.Mapper<ProductCategoryDTO, ProductCategory>(obj));
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Product category succesfully created",
                data = null,
                error = null
            };
        }

        public ResultDTO UpdateProductCategory(int id, ProductCategoryDTO obj)
        {
            var existingProductCategory = _db.ProductCategories.FirstOrDefault(p => p.Id == id);
            if (existingProductCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product category not found",
                    data = null,
                    error = null
                };
            }
            if (existingProductCategory.Name == obj.Name)
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null,
                    error = null
                };
            }
            existingProductCategory.Name = obj.Name;
            _db.ProductCategories.Update(existingProductCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product category updated succesfully",
                data = null,
                error = null
            };
        }

        public ResultDTO DeleteProductCategory(int id)
        {
            var productCategory = _db.ProductCategories.FirstOrDefault(p => p.Id == id);
            if (productCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Product category not found",
                    data = null,
                    error = null
                };
            }
            productCategory.IsActive = false;
            productCategory.DeletedAt = DateTime.Now;
            _db.ProductCategories.Update(productCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product category deleted succesfully",
                data = null,
                error = null
            };
        }
    }
}
