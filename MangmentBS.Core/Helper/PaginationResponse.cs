using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Helper
{
    public class PaginationResponse<TEntity>
    {
        public PaginationResponse(int pageSize, int pageIndex, int count, IEnumerable<TEntity> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            ItemsCount = count;
            Data = data;
            if (pageSize > 0)
                PagesCount = (int)Math.Ceiling((double)ItemsCount / PageSize);
            else
                PagesCount = 0;

        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int ItemsCount { get; set; }
        public int PagesCount { get; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
