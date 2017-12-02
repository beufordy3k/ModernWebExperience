using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SL.WebExperience.Test.Web.Models
{
    public class ApiControllerBase : Controller
    {
        protected static IQueryable<T> GetFilteredQuery<T>(IQueryable<T> countries, SearchParams searchParams) where T : INamedEntity
        {
            if (searchParams == null)
            {
                return countries;
            }

            if (searchParams.Name != null)
            {
                countries = countries.Where(c => string.Equals(c.Name, searchParams.Name, StringComparison.OrdinalIgnoreCase));
            }

            if (searchParams.StartsWith != null)
            {
                countries = countries.Where(c => c.Name.StartsWith(searchParams.StartsWith, StringComparison.OrdinalIgnoreCase));
            }

            return countries;
        }
    }
}