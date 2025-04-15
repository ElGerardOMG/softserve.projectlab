using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;

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

        public ProductVariant? GetProductVariantById(int id)
        {
            return _db.ProductVariants.FirstOrDefault(p => p.Id == id);
        }

        public bool AddProductVariant(ProductVariantDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _db.ProductVariants.Add(DtoMapper.Mapper<ProductVariantDTO, ProductVariant>(obj));
            _db.SaveChanges();
            return true;
        }

        public bool UpdateProductVariant(int id, ProductVariantDTO obj)
        {
            var existingProductVariant = _db.ProductVariants.FirstOrDefault(p => p.Id == id);
            if (existingProductVariant == null)
            {
                return false;
            }
            existingProductVariant.SubName = obj.SubName;
            existingProductVariant.SubFamily = obj.SubFamily;
            existingProductVariant.Price = obj.Price;
            existingProductVariant.Sku = obj.Sku;
            _db.ProductVariants.Update(existingProductVariant);
            _db.SaveChanges();
            return true;
        }

        public bool DeleteProductVariant(int id)
        {
            var productVariant = _db.ProductVariants.FirstOrDefault(p => p.Id == id);
            if (productVariant == null)
            {
                return false;
            }
            _db.ProductVariants.Remove(productVariant);
            _db.SaveChanges();
            return true;
        }
    }
}
