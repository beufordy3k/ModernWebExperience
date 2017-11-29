namespace SL.WebExperience.Test.Web.Models
{
    public class PagingParams
    {
        private const int DefaultPageNumber = 1;
        private const int DefaultPageSize = 10;


        public int PageNumber { get; set; } = DefaultPageNumber;
        public int PageSize { get; set; } = DefaultPageSize;
    }
}