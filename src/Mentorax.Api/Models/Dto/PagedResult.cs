using System.Collections.Generic;

namespace Mentorax.Api.Models.Dto
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }

        public int TotalPages =>
            PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedResult(IEnumerable<T> items, int page, int pageSize, long totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
