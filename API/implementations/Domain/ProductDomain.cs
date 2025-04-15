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
        public PaginatedResponseDTO<Product> GetAllProducts(bool isActive, FilterDTO filters, string type, int page, int pageSize)
        {
            return Filter.ProductQueryProcessor(_db.Products, _db, filters, type, isActive, page, pageSize);
        }

        public async Task<Product>? GetProductByID(int id)
        {
            return _db.Products.Include(Product => Product.ProductVariants).FirstOrDefault(p => p.Id == id);
        }

        public bool AddProduct(ProductDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            // EXTRAER LISTA DE VARIANTES DEL PRODUCTO
            ICollection<ProductVariantDTO> productVariants = DtoMapper.ExtractCollection<ProductDTO, ProductVariantDTO>(obj);
            // CASTEAR EL DTO A UN OBJETO DE PRODUCTO
            using (Product newProduct = DtoMapper.Mapper<ProductDTO, Product>(obj)) 
            {
                // REGISTRAR EL PRODUCTO
                _db.Products.Add(newProduct);
                // GUARDARLO PARA OBTENER SU ID
                _db.SaveChanges(); 
                // ITERAR LAS VARIANTES DE PRODUCTO
                foreach (var variant in productVariants)
                {
                    // CASTEAR EL DTO A UN OBJETO DE PRODUCT VARIANT
                    ProductVariant newProductVariant = DtoMapper.Mapper<ProductVariantDTO, ProductVariant>(variant); 
                    // EXTRAER LISTA DE ATRIBUTOS DE LA VARIANTE
                    ICollection<AttributeDTO> productAttributes = DtoMapper.ExtractCollection<ProductVariantDTO, AttributeDTO>(variant);
                    // ASIGNARLE LA ID DEL PRODUCTO BASE A LA VARIANTE
                    newProductVariant.IdProduct = newProduct.Id;
                    // REGISTRAR LA VARIANTE
                    newProduct.ProductVariants.Add(newProductVariant);
                    // GUARDARLA PARA OBTENER SU ID
                    _db.SaveChanges();
                    // ITERAR LOS ATRIBUTOS DE LA VARIANTE
                    foreach (var attribute in productAttributes)
                    {
                        // CASTEAR EL DTO A UN OBJETO DE ATRIBUTO
                        Models.Attribute newProductAttribute = DtoMapper.Mapper<AttributeDTO, Models.Attribute>(attribute);
                        // ASIGNARLE LA ID DEL PRODUCTO BASE Y DE LA VARIANTE
                        newProductAttribute.IdProduct = newProduct.Id;
                        newProductAttribute.IdProductVariant = newProductVariant.Id;
                        // REGISTRAR EL ATRIBUTO
                        _db.Attributes.Add(newProductAttribute);
                        // GUARDARLO EL ATRIBUTO
                        _db.SaveChanges();
                    }
                }
            }
            _db.SaveChanges();
            return true;
        }
        public bool UpdateProduct(int id, ProductDTO obj)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.ProductType = obj.ProductType;
            existingProduct.ProductCategory = obj.ProductCategory;
            existingProduct.Name = obj.Name;
            existingProduct.Brand = obj.Brand;
            existingProduct.Family = obj.Family;

            _db.Products.Update(existingProduct);
            _db.SaveChanges();
            return true;
        }
        public bool DeleteProduct(int id)
        {
            var existingProduct = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.IsActive = false;

            _db.Products.Update(existingProduct);
            _db.SaveChanges();
            return true;
        }
    }
}
