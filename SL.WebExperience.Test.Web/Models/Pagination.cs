namespace SL.WebExperience.Test.Web.Models
{

    /// <summary>
    /// Used to store the pagination information for the returned results
    /// </summary>
    public class Pagination
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int LastPageNumber { get; set; }
        public string NextPageLink { get; set; }
        public string PreviousPageLink { get; set; }
        public int PageStartRecordNumber { get; set; }
        public int PageEndRecordNumber { get; set; }
    }
}