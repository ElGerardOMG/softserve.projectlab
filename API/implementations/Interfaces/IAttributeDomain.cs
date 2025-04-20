using API.Models;
using API.DTOs;
namespace API.implementations.Interfaces
{
    public interface IAttributeDomain
    {
        ResultDTO AddAttribute(AttributeDTO obj);
        ResultDTO UpdateAttribute(int id_attribute, string field, string value);
        ResultDTO DeleteAttribute(int id_attribute);
    }
}
