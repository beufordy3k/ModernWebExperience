using System.Linq;
using System.Threading.Tasks;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResult<T>(this IQueryable<T> source, int pageNumber, int pageSize, int maxResultCount)
        {
            var pagedResult = new PagedResult<T>(pageNumber, pageSize);

            await pagedResult.ExecuteQuery(source, maxResultCount);

            return pagedResult;
        }
    }
}