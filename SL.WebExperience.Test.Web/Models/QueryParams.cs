namespace SL.WebExperience.Test.Web.Models
{
    public class QueryParams
    {
        private const int DefaultPageNumber = 1;
        private  const int DefaultPageSize = 10;

        public int? Country { get; set; }
        public int? MimeType { get; set; }

        public int PageNumber { get; set; } = DefaultPageNumber;
        public int PageSize { get; set; } = DefaultPageSize;


    }
}