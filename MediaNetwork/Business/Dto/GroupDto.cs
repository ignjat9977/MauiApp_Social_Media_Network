using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class GroupDto
    {
        public int Id { get; set; }
        public int PrivacyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
