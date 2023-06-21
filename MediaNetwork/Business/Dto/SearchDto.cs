using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class SearchDto
    {
        public string? Keyword { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool? HasComments { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
    }
}
