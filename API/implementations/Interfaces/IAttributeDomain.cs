using API.Models;

namespace API.implementations.Interfaces
{
    public interface IAttributeDomain
    {
        bool AddAttribute(int id_product, Models.Attribute obj);
        bool UpdateAttribute(int id_attribute, Models.Attribute obj);
        bool DeleteAttribute(int id_attribute);
    }
}
