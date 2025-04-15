using API.Data;
using API.implementations.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.implementations.Domain
{
    public class AttributeDomain : Controller, IAttributeDomain
    {
        private readonly ProjectlabContext _db;
        public AttributeDomain(ProjectlabContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public bool AddAttribute(int product_id, Models.Attribute attribute)
        {
            return true;
        }

        public bool UpdateAttribute(int attribute_id, Models.Attribute attribute)
        {
            return true;
        }

        public bool DeleteAttribute(int attribute_id)
        {
            return true;
        }

    }
}
