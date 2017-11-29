using Newtonsoft.Json;

namespace SL.WebExperience.Test.Web.Models
{

    /// <summary>
    /// Used to store the pagination information for the returned results
    /// </summary>
    public class Pagination
    {
        [JsonProperty("total")]
        public int TotalItems { get; set; }
        [JsonProperty("per_page")]
        public int PageSize { get; set; }
        [JsonProperty("current_page")]
        public int PageNumber { get; set; }
        [JsonProperty("last_page")]
        public int LastPageNumber { get; set; }
        [JsonProperty("next_page_url")]
        public string NextPageLink { get; set; }
        [JsonProperty("prev_page_url")]
        public string PreviousPageLink { get; set; }
        [JsonProperty("from")]
        public int PageStartRecordNumber { get; set; }
        [JsonProperty("to")]
        public int PageEndRecordNumber { get; set; }
    }
}