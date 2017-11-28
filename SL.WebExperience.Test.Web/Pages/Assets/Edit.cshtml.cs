using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Pages.Assets
{
    public class EditModel : PageModel
    {
        private readonly SL.WebExperience.Test.Web.Models.AssetDbContext _context;

        public EditModel(SL.WebExperience.Test.Web.Models.AssetDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name");
           ViewData["MimeTypeId"] = new SelectList(_context.MimeType, "MimeTypeId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./Index");
        }
    }
}
