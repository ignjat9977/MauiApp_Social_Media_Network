using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class PageResponse<T>
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}

