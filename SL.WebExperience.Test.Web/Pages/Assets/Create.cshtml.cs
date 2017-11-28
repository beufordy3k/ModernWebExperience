using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Pages.Assets
{
    public class CreateModel : PageModel
    {
        private readonly SL.WebExperience.Test.Web.Models.AssetDbContext _context;

        public CreateModel(SL.WebExperience.Test.Web.Models.AssetDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Name");
        ViewData["MimeTypeId"] = new SelectList(_context.MimeType, "MimeTypeId", "Name");
            return Page();
        }

        [BindProperty]
        public Asset Asset { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Asset.Add(Asset);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}