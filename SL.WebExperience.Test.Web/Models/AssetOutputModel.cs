using System.Collections.Generic;

namespace SL.WebExperience.Test.Web.Models
{
    public class AssetOutputModel
    {
        public Pagination Paging { get; set; }
        public IEnumerable<Asset> Data { get; set; }
    }
}