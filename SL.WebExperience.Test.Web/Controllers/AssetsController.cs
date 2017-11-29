using System.Linq;
using System.Threading.Tasks;

using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SL.WebExperience.Test.Web.Extensions;
using SL.WebExperience.Test.Web.Models;

namespace SL.WebExperience.Test.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AssetsController : Controller
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageNumber = 1;
        private const int MaxPageSize = 100;
        private const int MaxRecordCount = 2500;

        private readonly AssetDbContext _context;

        public AssetsController(AssetDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets(QueryParams queryParams = null)
        {
            var pageSize = queryParams?.PageSize ?? DefaultPageSize;
            var pageNumber = queryParams?.PageNumber ?? DefaultPageNumber;

            pageSize = pageSize > MaxPageSize
                ? MaxPageSize
                : pageSize;

            var query = GetFilteredQuery(_context.Asset, queryParams);

            //TODO: Convert to service
            var result = await query
                .Where(a => !a.IsDeleted)
                .Include(a => a.Country)
                .Include(a => a.MimeType)
                .ToPagedResult(pageNumber, pageSize, MaxRecordCount);

            var totalItemCount = result.TotalItems;

            pageNumber = result.PageNumber;

            var startRecordNumber = (pageNumber - 1) * pageSize + 1;
            var endRecordNumber = (pageNumber - 1) * pageSize + pageSize;

            endRecordNumber = endRecordNumber > totalItemCount ? totalItemCount : endRecordNumber;

            var output = new AssetOutputModel
            {
                Paging = new Pagination
                {
                    TotalItems = totalItemCount,
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    LastPageNumber = result.TotalPages,
                    NextPageLink = result.HasNextPage ? 
                        queryParams.ToUrl(Request.GetUri(), pageNumber + 1, pageSize)
                        : null,
                    PreviousPageLink = result.HasPreviousPage ?
                        queryParams.ToUrl(Request.GetUri(), pageNumber - 1, pageSize) 
                        : null,
                    PageStartRecordNumber = startRecordNumber,
                    PageEndRecordNumber = endRecordNumber
                },
                Data = result.Items
            };

            return Ok(output);
        }

        private static IQueryable<Asset> GetFilteredQuery(IQueryable<Asset> assets, QueryParams queryParams)
        {
            if (queryParams == null)
            {
                return assets;
            }

            if (queryParams.Country != null)
            {
                assets = assets.Where(a => a.CountryId == queryParams.Country.Value);
            }

            if (queryParams.MimeType != null)
            {
                assets = assets.Where(a => a.MimeTypeId == queryParams.MimeType.Value);
            }

            return assets;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = await _context.Asset
                .Include(a => a.Country)
                .Include(a => a.MimeType)
                .SingleOrDefaultAsync(m => m.AssetId == id);
                

            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset([FromRoute] int id, [FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asset.AssetId)
            {
                return BadRequest();
            }

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsset([FromBody] Asset asset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Asset.Add(asset);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AssetExists(asset.AssetId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAsset", new { id = asset.AssetId }, asset);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.AssetId == id);
            if (asset == null)
            {
                return NotFound();
            }

            asset.IsDeleted = true;
            _context.Entry(asset).State = EntityState.Modified;
            
            //_context.Asset.Remove(asset);
            await _context.SaveChangesAsync();

            return Ok(asset);
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.AssetId == id);
        }
    }
}