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
    public class DetailsModel : PageModel
    {
        private readonly SL.WebExperience.Test.Web.Models.AssetDbContext _context;

        public DetailsModel(SL.WebExperience.Test.Web.Models.AssetDbContext context)
        {
            _context = context;
        }

        public Asset Asset { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Asset = await _context.Asset
                .Include(a => a.Country)
                .Include(a => a.MimeType).SingleOrDefaultAsync(m => m.AssetId == id);

            if (Asset == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
