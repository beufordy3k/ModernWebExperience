﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace SL.WebExperience.Test.Web.Models
{
    public class PagedResult<T>
    {

        public int PageNumber { get; private set; }
        public int PageSize { get; }

        public int TotalItems { get; private set; }
        public List<T> Items { get; private set; }

        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber => HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber => HasPreviousPage ? PageNumber - 1 : 1;

        public PagedResult(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public async Task ExecuteQuery(IQueryable<T> source, int maxResultCount)
        {
            TotalItems = await source
                .Take(maxResultCount)
                .CountAsync();

            var skipCount = PageSize * (PageNumber - 1);

            if (skipCount > TotalItems)
            {
                skipCount = TotalItems - PageSize; //if request takes us beyond the end of the results
                PageNumber = TotalPages;

                if (skipCount < 0)
                {
                    skipCount = 0;
                    PageNumber = 1;
                }
            }

            Items = await source
                .Skip(skipCount)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}