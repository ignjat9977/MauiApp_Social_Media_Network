using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public IEnumerable<string>? ImagesPath { get; set; }
        public string ImageUrl => ImagesPath.FirstOrDefault();
    }
}
