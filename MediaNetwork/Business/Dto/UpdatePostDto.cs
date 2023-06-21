using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int PrivacyId { get; set; }
    }
    public class CreatePostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int PrivacyId { get; set; }
    }
}