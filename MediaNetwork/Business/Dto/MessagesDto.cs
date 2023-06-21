using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class MessagesDto
    {
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string SenderName { get; set; }
        public string ReciverName { get; set; }
        public IEnumerable<MessageDto> FirstThreeMessages { get;set;}
    }
    public class MessageDto
    {
        public int SenderId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
