﻿using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAssets(FilteringParams filteringParams = null, PagingParams pagingParams = null)
        {
            var pageSize = pagingParams?.PageSize ?? DefaultPageSize;
            var pageNumber = pagingParams?.PageNumber ?? DefaultPageNumber;

            pageSize = pageSize > MaxPageSize
                ? MaxPageSize
                : pageSize;

            var skipCount = pageSize * (pageNumber - 1);

            skipCount = skipCount > MaxRecordCount ? MaxRecordCount - pageSize : skipCount;

            var query = GetFilteredQuery(_context.Asset, filteringParams);

            //TODO: Convert to service
            var result = await query
                .Include(a => a.Country) //TODO: Remove nested asset record
                .Include(a => a.MimeType) //TODO: Remove nested asset record
                .ToPagedResult(pageNumber, pageSize, MaxRecordCount);

            var totalItemCount = result.TotalItems;

            var endRecordNumber = skipCount + pageSize;

            endRecordNumber = endRecordNumber > totalItemCount ? totalItemCount : endRecordNumber;

            var output = new AssetOutputModel
            {
                Paging = new Pagination
                {
                    TotalItems = totalItemCount,
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    LastPageNumber = result.TotalPages,
                    NextPageLink = result.HasNextPage ? $"pageNumber={pageNumber+1}&pageSize={pageSize}" : null, //TODO: Need Url builder
                    PreviousPageLink = result.HasPreviousPage ? $"pageNumber={pageNumber-1}&pageSize={pageSize}" : null,
                    PageStartRecordNumber = skipCount + 1,
                    PageEndRecordNumber = endRecordNumber
                },
                Data = result.Items
            };

            return Ok(output);
        }

        private static IQueryable<Asset> GetFilteredQuery(IQueryable<Asset> assets, FilteringParams filteringParams)
        {
            if (filteringParams == null)
            {
                return assets;
            }

            if (filteringParams.Country != null)
            {
                assets = assets.Where(a => a.CountryId == filteringParams.Country.Value);
            }

            if (filteringParams.MimeType != null)
            {
                assets = assets.Where(a => a.MimeTypeId == filteringParams.MimeType.Value);
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

            var asset = await _context.Asset.SingleOrDefaultAsync(m => m.AssetId == id);

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

            _context.Asset.Remove(asset);
            await _context.SaveChangesAsync();

            return Ok(asset);
        }

        private bool AssetExists(int id)
        {
            return _context.Asset.Any(e => e.AssetId == id);
        }
    }
}