using API.DTOs;
using API.Models;

namespace API.implementations.Interfaces
{
    public interface IAttributeCategoryDomain
    {
        ResultDTO GetAllCategories(bool? isActive);
        ResultDTO GetAttributeCategoryById(int id);
        ResultDTO AddAttributeCategory(AttributeCategoryDTO obj);
        ResultDTO UpdateAttributeCategory(int id, AttributeCategoryDTO obj);
        ResultDTO DeleteAttributeCategory(int id);
    }
}
