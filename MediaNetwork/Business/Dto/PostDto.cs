using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class PostDto
    {
        public int? Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int PrivacyId { get; set; }
        public IEnumerable<string>? Path { get; set; }
        public string PathToShow => Path.FirstOrDefault();
        public DateTime CreatedAt { get; set; }
        public int? LikesCounter { get; set; }
        public IEnumerable<CommentDto>? Comments { get; set; }
        public bool ShowPicture => Path.Any();
    }
    public class Pagination
    {
        public int CurrentPage { get; set; }
    }
}
