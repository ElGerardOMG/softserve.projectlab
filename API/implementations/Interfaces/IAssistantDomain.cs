using API.DTOs;

namespace API.implementations.Interfaces
{
    public interface IAssistantDomain
    {
        public ResultDTO Ask(string prompt);
    }
}
