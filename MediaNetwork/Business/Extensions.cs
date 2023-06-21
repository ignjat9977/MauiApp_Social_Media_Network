using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business
{
    public static class Extensions
    {
        public async static Task<User> GetUser(this ISecureStorage storage)
        {
            var userString = storage.GetAsync("user").GetAwaiter().GetResult();

            if (userString == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<User>(userString);
        }
        public static DateTime ToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
