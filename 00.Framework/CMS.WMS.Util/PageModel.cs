using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sy.Util
{
    public class PageModel
    {
        public List<Search> Search { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 50;

        public string Sort { get; set; }

        public PageModel()
        {
            Search = new List<Search>();
        }
    }

    public class Search
    {
        public string Operation { get; set; }

        public string Col { get; set; }

        public string Val { get; set; }
    }
}
