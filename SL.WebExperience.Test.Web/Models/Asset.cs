using System;

namespace SL.WebExperience.Test.Web.Models
{
    public partial class Asset
    {
        public DateTimeOffset CreatedWhen { get; set; }
        public DateTimeOffset UpdatedWhen { get; set; }
        public string Version { get; set; }
        public int AssetId { get; set; }
        public Guid AssetKey { get; set; }
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int MimeTypeId { get; set; }
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }

        public Country Country { get; set; }
        public MimeType MimeType { get; set; }
    }
}
