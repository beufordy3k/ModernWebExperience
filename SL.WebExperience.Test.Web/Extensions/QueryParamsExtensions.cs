using System;
using Flurl;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Extensions
{
    public static class QueryParamsExtensions
    {
        public static string ToUrl(this QueryParams queryParams, Uri requestUri, int? pageNumber = null, int? pageSize = null)
        {
            var urlPath = $"{requestUri.Scheme}://{requestUri.Authority}";

            pageNumber = pageNumber ?? queryParams?.PageNumber;
            pageSize = pageSize ?? queryParams?.PageSize;

            urlPath = urlPath
                .AppendPathSegment(requestUri.AbsolutePath)
                .SetQueryParams(new
                {
                    country = queryParams?.Country,
                    mimeType = queryParams?.MimeType,
                    pageNumber,
                    pageSize
                });

            return urlPath;
        }
    }
}