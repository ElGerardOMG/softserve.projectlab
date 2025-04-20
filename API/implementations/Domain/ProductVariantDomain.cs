using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.EntityFrameworkCore;

namespace API.implementations.Domain
{
    public class ProductVariantDomain : IProductVariantDomain
    {
        private readonly ProjectlabContext _db;
        public ProductVariantDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // PRODUCT VARIANTS

        public ResultDTO GetProductVariantById(int id)
        {
            return new ResultDTO
            {
                statusCode = 201,
                description = "Product variant detail",
                data = _db.ProductVariants.Include(ProductVariant => ProductVariant.Attributes).FirstOrDefault(p => p.Id == id),
                error = null
            };
        }

        public ResultDTO AddProductVariant(ProductVariantDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 201,
                    description = "Invalid product variant object",
                    data = null,
                    error = null
                };
            }
            _db.ProductVariants.Add(DtoMapper.Mapper<ProductVariantDTO, ProductVariant>(obj));
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Product variant created successfully",
                data = null,
                error = null
            };
        }

        public ResultDTO UpdateProductVariant(int id, ProductVariantDTO obj)
        {
            var existingProductVariant = _db.ProductVariants.FirstOrDefault(p => p.Id == id);
            if (existingProductVariant == null)
            {
                return new ResultDTO
                {
                    statusCode = 201,
                    description = "Product not found",
                    data = null,
                    error = null
                };
            }
            existingProductVariant.SubName = obj.SubName;
            existingProductVariant.SubFamily = obj.SubFamily;
            existingProductVariant.Price = obj.Price;
            existingProductVariant.Sku = obj.Sku;
            _db.ProductVariants.Update(existingProductVariant);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Product variant updated successfully",
                data = null,
                error = null
            };
        }

        public ResultDTO DeleteProductVariant(int id)
        {
            var productVariant = _db.ProductVariants.FirstOrDefault(p => p.Id == id);
            if (productVariant == null)
            {
                return new ResultDTO
                {
                    statusCode = 201,
                    description = "Product not found",
                    data = null,
                    error = null
                };
            }
            productVariant.IsActive = false;
            productVariant.DeletedAt = DateTime.Now;
            _db.ProductVariants.Update(productVariant);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Product variant deleted successfully",
                data = null,
                error = null
            };
        }
    }
}
