using API.Data;
using API.implementations.Interfaces;
using API.Models;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.Utils.Implementations;

namespace API.implementations.Domain
{
    public class AttributeDomain : Controller, IAttributeDomain
    {
        private readonly ProjectlabContext _db;
        public AttributeDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ResultDTO AddAttribute(AttributeDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid attribute object",
                    data = null,
                    error = null
                };
            }
            _db.Attributes.Add(DtoMapper.Mapper<AttributeDTO, Models.Attribute>(obj));
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Attribute succesfully created",
                data = null,
                error = null
            };
        }

        public ResultDTO UpdateAttribute(int id, string field, string value)
        {
            var existingProductCategory = _db.Attributes.FirstOrDefault(p => p.Id == id);
            if (existingProductCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Attribute not found",
                    data = null,
                    error = null
                };
            }
            existingProductCategory.Field = field;
            existingProductCategory.Value = value;
            existingProductCategory.UpdateAt = DateTime.Now;
            _db.Attributes.Update(existingProductCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Attribute succesfully updated",
                data = null,
                error = null
            };
        }

        public ResultDTO DeleteAttribute(int id)
        {
            var productCategory = _db.Attributes.FirstOrDefault(p => p.Id == id);
            if (productCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Attribute not found",
                    data = null,
                    error = null
                };
            }
            productCategory.IsActive = false;
            productCategory.DeletedAt = DateTime.Now;
            _db.Attributes.Update(productCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Attribute succesfully deleted",
                data = null,
                error = null
            };
        }

    }
}
