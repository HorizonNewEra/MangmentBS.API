using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Params
{
    public class SpecificationsParams
    {
        private string? sort;

        public string? Sort
        {
            get { return sort; }
            set { sort = value?.ToLower(); }
        }
        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }
        public int PageSize { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
    }
}
