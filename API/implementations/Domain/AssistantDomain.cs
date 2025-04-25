using API.Data;
using API.implementations.Interfaces;
using API.Models;
using API.DTOs;
using API.Utils.Implementations;

namespace API.implementations.Domain
{
    public class AssistantDomain : IAssistantDomain
    {
        private readonly ProjectlabContext _db;
        public AssistantDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public ResultDTO Ask(string prompt)
        {
            string recommendation = Gemini.ObtenerRecomendacion(prompt, _db);
            return new ResultDTO
            {
                statusCode = 200,
                description = "Assistant recommendation",
                data = recommendation,
                error = null
            };
        }
    }
}
