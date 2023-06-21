using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business.Dto
{
    public class RegisterDto
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Password { get; set; } 
        public string Email { get; set; } 
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
