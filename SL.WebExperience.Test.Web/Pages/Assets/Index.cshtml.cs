using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Pages.Assets
{
    public class IndexModel : PageModel
    {
        private readonly SL.WebExperience.Test.Web.Models.AssetDbContext _context;

        public IndexModel(SL.WebExperience.Test.Web.Models.AssetDbContext context)
        {
            _context = context;
        }

        public IList<Asset> Asset { get;set; }

        public async Task OnGetAsync()
        {
            Asset = await _context.Asset
                .Take(100)
                .Include(a => a.Country)
                .Include(a => a.MimeType).ToListAsync();
        }
    }
}
