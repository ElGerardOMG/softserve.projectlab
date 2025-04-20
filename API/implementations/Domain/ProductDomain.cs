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
            // EXTRACT LIST OF PRODUCT VARIANTS
            ICollection<ProductVariantDTO> productVariants = DtoMapper.ExtractCollection<ProductDTO, ProductVariantDTO>(obj);
            // CAST THE DTO TO A PRODUCT OBJECT
            using (Product newProduct = DtoMapper.Mapper<ProductDTO, Product>(obj))
            {
                // REGISTER THE PRODUCT
                _db.Products.Add(newProduct);
                // SAVE TO GET ITS ID
                _db.SaveChanges();
                // ITERATE THROUGH THE PRODUCT VARIANTS
                foreach (var variant in productVariants)
                {
                    // CAST THE DTO TO A PRODUCT VARIANT OBJECT
                    ProductVariant newProductVariant = DtoMapper.Mapper<ProductVariantDTO, ProductVariant>(variant);
                    // EXTRACT LIST OF ATTRIBUTES FROM THE VARIANT
                    ICollection<AttributeDTO> productAttributes = DtoMapper.ExtractCollection<ProductVariantDTO, AttributeDTO>(variant);
                    // ASSIGN THE BASE PRODUCT ID TO THE VARIANT
                    newProductVariant.IdProduct = newProduct.Id;
                    // REGISTER THE VARIANT
                    newProduct.ProductVariants.Add(newProductVariant);
                    // SAVE TO GET ITS ID
                    _db.SaveChanges();
                    // ITERATE THROUGH THE VARIANT'S ATTRIBUTES
                    foreach (var attribute in productAttributes)
                    {
                        // CAST THE DTO TO AN ATTRIBUTE OBJECT
                        Models.Attribute newProductAttribute = DtoMapper.Mapper<AttributeDTO, Models.Attribute>(attribute);
                        // ASSIGN THE BASE PRODUCT ID AND VARIANT ID TO THE ATTRIBUTE
                        newProductAttribute.IdProduct = newProduct.Id;
                        newProductAttribute.IdProductVariant = newProductVariant.Id;
                        // REGISTER THE ATTRIBUTE
                        _db.Attributes.Add(newProductAttribute);
                        // SAVE THE ATTRIBUTE
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
