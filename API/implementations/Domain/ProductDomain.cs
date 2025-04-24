// Microsoft.AspNetCore.Http;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.DTOs;
using API.Utils.Implementations;
using API.implementations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.implementations.Domain
{
    public class ProductDomain : Controller, IProductDomain
    {
        private readonly ProjectlabContext _db;
        public ProductDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // PRODUCTS
        public ResultDTO GetAllProducts(bool isActive, FilterDTO filters, string type, int page, int pageSize)
        {
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product list",
                data = Filter.ProductQueryProcessor(_db.Products, _db, filters, type, isActive, page, pageSize),
                error = null
            };
        }

        public ResultDTO GetProductByID(int id)
        {
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product detail",
                data = _db.Products.Include(Product => Product.ProductVariants).FirstOrDefault(p => p.Id == id),
                error = null
            };
        }

        public ResultDTO AddProduct(ProductDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid object",
                    data = null,
                    error = null
                };
            }
            ICollection<ProductVariantDTO> productVariants = DtoMapper.ExtractCollection<ProductDTO, ProductVariantDTO>(obj);
            using (Product newProduct = DtoMapper.Mapper<ProductDTO, Product>(obj))
            {
                _db.Products.Add(newProduct);
                _db.SaveChanges();
                foreach (var variant in productVariants)
                {
                    ProductVariant newProductVariant = DtoMapper.Mapper<ProductVariantDTO, ProductVariant>(variant);
                    ICollection<AttributeDTO> productAttributes = DtoMapper.ExtractCollection<ProductVariantDTO, AttributeDTO>(variant);
                    newProductVariant.IdProduct = newProduct.Id;
                    newProduct.ProductVariants.Add(newProductVariant);
                    _db.SaveChanges();
                    foreach (var attribute in productAttributes)
                    {
                        Models.Attribute newProductAttribute = DtoMapper.Mapper<AttributeDTO, Models.Attribute>(attribute);
                        newProductAttribute.IdProduct = newProduct.Id;
                        newProductAttribute.IdProductVariant = newProductVariant.Id;
                        _db.Attributes.Add(newProductAttribute);
                        _db.SaveChanges();
                    }
                }
            }

            _db.SaveChanges();
            return new ResultDTO {
                statusCode = 201,
                description = "Product created successfully",
                data = null,
                error = null
            };
        }
        public ResultDTO UpdateProduct(int id, ProductDTO obj)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    error = "Error while updating the product"
                };
            }
            if (Comparer.Compare<ProductDTO, Product>(obj, existingProduct))
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null,
                    error = null
                };
            }
            existingProduct.ProductType = obj.ProductType;
            existingProduct.ProductCategory = obj.ProductCategory;
            existingProduct.Name = obj.Name;
            existingProduct.Brand = obj.Brand;
            existingProduct.Family = obj.Family;

            _db.Products.Update(existingProduct);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product updated successfully",
            };
        }
        public ResultDTO DeleteProduct(int id)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Error while deleting the product",
                    data = null,
                    error = null
                };
            }

            existingProduct.IsActive = false;
            existingProduct.DeletedAt = DateTime.Now;

            _db.Products.Update(existingProduct);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Product deleted successfully",
                data = null,
                error = null
            };
        }
    }
}
