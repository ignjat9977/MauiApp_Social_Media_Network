using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    internal class FriendsAndSuggestedFriens
    {

        public IEnumerable<UserDto> Friends { get; set; }
        public IEnumerable<UserDto> FriendsOf { get; set; }
    }
}
