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

        public List<AttributeCategory> GetAllCategories(bool? isActive)
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
            return attributeCategories;
        }

        public async Task<AttributeCategory>? GetAttributeCategoryById(int id)
        {
            return _db.AttributeCategories.FirstOrDefault(p => p.Id == id);
        }


        // ONLY DEVELOPMENT TOOLS

        public bool AddAttributeCategory(AttributeCategoryDTO obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            _db.AttributeCategories.Add(DtoMapper.Mapper<AttributeCategoryDTO, AttributeCategory>(obj));
            _db.SaveChanges();
            return true;
        }

        public bool UpdateAttributeCategory(int id, AttributeCategoryDTO obj)
        {
            var existingAttributeCategory = _db.AttributeCategories.FirstOrDefault(p => p.Id == id);
            if (existingAttributeCategory == null)
            {
                return false;
            }
            existingAttributeCategory.Name = obj.Name;
            _db.AttributeCategories.Update(existingAttributeCategory);
            _db.SaveChanges();
            return true;
        }
        public bool DeleteAttributeCategory(int id)
        {
            var existingAttributeCategory = _db.AttributeCategories.FirstOrDefault(p => p.Id == id);
            if (existingAttributeCategory == null)
            {
                return false;
            }
            existingAttributeCategory.IsActive = false;
            _db.AttributeCategories.Update(existingAttributeCategory);
            _db.SaveChanges();
            return true;
        }
    }
}
