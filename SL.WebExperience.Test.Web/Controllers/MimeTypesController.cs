using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/MimeTypes")]
    public class MimeTypesController : ApiControllerBase
    {
        private readonly AssetDbContext _context;

        public MimeTypesController(AssetDbContext context)
        {
            _context = context;
        }

        // GET: api/MimeTypes
        [HttpGet]
        public async Task<IActionResult> GetMimeTypes(SearchParams searchParams = null)
        {
            var query = _context.MimeType as IQueryable<MimeType>;

            query = GetFilteredQuery(query, searchParams);

            var mimeTypes = await query
                .OrderBy(m => m.Name)
                .ToArrayAsync();

            return Ok(mimeTypes);
        }

        // GET: api/MimeTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMimeType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mimeType = await _context.MimeType.SingleOrDefaultAsync(m => m.MimeTypeId == id);

            if (mimeType == null)
            {
                return NotFound();
            }

            return Ok(mimeType);
        }

        //// PUT: api/MimeTypes/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMimeType([FromRoute] int id, [FromBody] MimeType mimeType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != mimeType.MimeTypeId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mimeType).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MimeTypeExists(id))
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

        //// POST: api/MimeTypes
        //[HttpPost]
        //public async Task<IActionResult> PostMimeType([FromBody] MimeType mimeType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.MimeType.Add(mimeType);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (MimeTypeExists(mimeType.MimeTypeId))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetMimeType", new { id = mimeType.MimeTypeId }, mimeType);
        //}

        //// DELETE: api/MimeTypes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMimeType([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var mimeType = await _context.MimeType.SingleOrDefaultAsync(m => m.MimeTypeId == id);
        //    if (mimeType == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.MimeType.Remove(mimeType);
        //    await _context.SaveChangesAsync();

        //    return Ok(mimeType);
        //}

        //private bool MimeTypeExists(int id)
        //{
        //    return _context.MimeType.Any(e => e.MimeTypeId == id);
        //}
    }
}