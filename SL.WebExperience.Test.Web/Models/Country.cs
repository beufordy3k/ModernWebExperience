using System;
using Newtonsoft.Json;

namespace SL.WebExperience.Test.Web.Models
{
    public partial class Country : INamedEntity
    {
        public Country()
        {
            //Asset = new HashSet<Asset>();
        }

        public DateTimeOffset CreatedWhen { get; set; }
        public DateTimeOffset UpdatedWhen { get; set; }
        public string Version { get; set; }
        [JsonProperty("id")]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public Guid CountryKey { get; set; }

        //public ICollection<Asset> Asset { get; set; }
    }
}
