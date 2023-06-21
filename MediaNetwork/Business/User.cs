using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business
{
    public class User
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime LoginExpiration { get; set; }
        public bool ShouldBeLoggedOut => LoginExpiration < DateTime.UtcNow;
    }
}
