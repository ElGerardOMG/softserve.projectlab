using Microsoft.Extensions.Diagnostics.HealthChecks;
using API.DTOs;
using API.Models;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Utils.Implementations
{
    public class Filter
    {
        public static PaginatedResponseDTO<Product> ProductQueryProcessor(
            IQueryable<Product> list,
            ProjectlabContext db, 
            FilterDTO filters, 
            string type,
            bool isActive,
            int page = 1, 
            int pageSize = 10)
        {
            var query = db.Products.AsQueryable();

            query = query.Where(p => p.ProductType == type);

            if (isActive)
            {
                query = query.Where(p => p.IsActive == true);
            }

            if (filters != null && filters.Filters != null || filters.Filters.Count > 0)
            {
                foreach (var filter in filters.Filters)
                {
                    if (filter.IsLike == true)
                    {
                        query = query.Where(p =>
                        db.Attributes.Any(a =>
                            a.IdProduct == p.Id &&
                            a.Field == filter.Name &&
                            a.Value.Contains(filter.Value)));
                    }
                    else
                    {
                        query = query.Where(p =>
                        db.Attributes.Any(a =>
                            a.IdProduct == p.Id &&
                            a.Field == filter.Name &&
                            a.Value == filter.Value));
                    }
                }
            }
            int fullQuertyLenght = query.Count();
            query = query
            .OrderBy(p => p.Id) // Siempre se debe ordenar antes de paginar
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

            return new PaginatedResponseDTO<Product>
            {
                Data = query.ToList(),
                PaginationData = new PaginationDataDTO
                {
                    TotalPages = (int)Math.Ceiling((double)query.Count() / pageSize),
                    TotalRecords = fullQuertyLenght,
                    CurrentPage = page,
                    PageSize = pageSize
                }
            };
        }
    }
}
