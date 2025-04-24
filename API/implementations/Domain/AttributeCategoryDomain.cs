using API.Data;
using API.DTOs;
using API.implementations.Interfaces;
using API.Models;
using API.Utils.Implementations;
using Microsoft.EntityFrameworkCore;

namespace API.implementations.Domain
{
    public class AttributeCategoryDomain : IAttributeCategoryDomain
    {
        private readonly ProjectlabContext _db;
        public AttributeCategoryDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ResultDTO GetAllCategories(bool? isActive)
        {
            List<AttributeCategory> attributeCategories = new List<AttributeCategory>();
            if (isActive == null)
            {
                attributeCategories = _db.AttributeCategories.ToList();
            }
            else
            {
                attributeCategories = _db.AttributeCategories.Where(x => x.IsActive == isActive).ToList();
            }
            return new ResultDTO
            {
                statusCode = 200,
                description = "Attribute category list",
                data = attributeCategories,
                error = null
            };
        }

        public ResultDTO GetAttributeCategoryById(int id)
        {
            return new ResultDTO
            {
                statusCode = 200,
                description = "Attribute category detail",
                data = _db.AttributeCategories.FirstOrDefault(p => p.Id == id),
                error = null
            };
        }


        // ONLY DEVELOPMENT TOOLS

        public ResultDTO AddAttributeCategory(AttributeCategoryDTO obj)
        {
            if (obj == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Invalid attribute category object",
                    data = null,
                    error = null
                };
            }
            _db.AttributeCategories.Add(DtoMapper.Mapper<AttributeCategoryDTO, AttributeCategory>(obj));
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Attribute category succesfully created",
                data = null,
                error = null
            };
        }

        public ResultDTO UpdateAttributeCategory(int id, AttributeCategoryDTO obj)
        {
            var existingAttributeCategory = _db.AttributeCategories.FirstOrDefault(p => p.Id == id);
            if (existingAttributeCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Attribute category not found",
                    data = null,
                    error = null
                };
            }
            if(Comparer.Compare<AttributeCategoryDTO, AttributeCategory>(obj, existingAttributeCategory))
            {
                return new ResultDTO
                {
                    statusCode = 200,
                    description = "No changes detected",
                    data = null,
                    error = null
                };
            }
            existingAttributeCategory.Name = obj.Name;
            _db.AttributeCategories.Update(existingAttributeCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 200,
                description = "Attribute category succesfully updated",
                data = null,
                error = null
            };
        }
        public ResultDTO DeleteAttributeCategory(int id)
        {
            var existingAttributeCategory = _db.AttributeCategories.FirstOrDefault(p => p.Id == id);
            if (existingAttributeCategory == null)
            {
                return new ResultDTO
                {
                    statusCode = 500,
                    description = "Attribute category not found",
                    data = null,
                    error = null
                };
            }
            existingAttributeCategory.IsActive = false;
            _db.AttributeCategories.Update(existingAttributeCategory);
            _db.SaveChanges();
            return new ResultDTO
            {
                statusCode = 201,
                description = "Attribute category succesfully deleted",
                data = null,
                error = null
            };
        }
    }
}
