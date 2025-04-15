using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IAttributeCategoryDomain
    {
        List<AttributeCategory> GetAllCategories(bool? isActive);
        Task<AttributeCategory>? GetAttributeCategoryById(int id);
        bool AddAttributeCategory(AttributeCategoryDTO obj);
        bool UpdateAttributeCategory(int id, AttributeCategoryDTO obj);
        bool DeleteAttributeCategory(int id);
    }
}
