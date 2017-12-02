using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Countries")]
    public class CountriesController : ApiControllerBase
    {
        private readonly AssetDbContext _context;

        public CountriesController(AssetDbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<IActionResult> GetCountries(SearchParams searchParams = null)
        {
            var query = _context.Country as IQueryable<Country>;

            query = GetFilteredQuery(query, searchParams);

            var countries = await query
                .OrderBy(c => c.Name)
                .ToArrayAsync();

            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var country = await _context.Country.SingleOrDefaultAsync(m => m.CountryId == id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        //// PUT: api/Countries/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry([FromRoute] int id, [FromBody] Country country)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != country.CountryId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Countries
        //[HttpPost]
        //public async Task<IActionResult> PostCountry([FromBody] Country country)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Country.Add(country);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (CountryExists(country.CountryId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
        //}

        //// DELETE: api/Countries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var country = await _context.Country.SingleOrDefaultAsync(m => m.CountryId == id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Country.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return Ok(country);
        //}

        //private bool CountryExists(int id)
        //{
        //    return _context.Country.Any(e => e.CountryId == id);
        //}
    }
}