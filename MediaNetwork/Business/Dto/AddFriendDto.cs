using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class AddFriendDto
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }
    }
}
