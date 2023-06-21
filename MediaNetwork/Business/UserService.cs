using MediaNetwork.Business.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business
{
    public class UserService : BaseService
    {
        public UserService()
        {
           
        }
        public async Task<UserDto> GetUserInfo(int? id)
        {
            

            var str = await SecureStorage.Default.GetAsync("user");
            var user = JsonConvert.DeserializeObject<User>(str);
            if (id == null)
            {
                id = user.Id;
            }
            RestRequest request = new RestRequest("Friend/" + id);
            request.AddHeader("Content-Type", "application/json");;
            request.AddHeader("Authorization", $"Bearer {user.Token}");

            var response = Client.Get<UserDto>(request);

            var data = response.Data;

            return data;
        }

        public async Task<PageResponse<UserDto>> GetUsers(string keyword)
        {
            if (keyword != null) {
                keyword = "?Keyword=" + keyword;
            }
            RestRequest request = new RestRequest("User" + keyword);

            request.AddHeader("Content-Type", "application/json");
            string token = await SecureStorage.Default.GetAsync("jwt");
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = Client.Get<PageResponse<UserDto>>(request);

            var data = response.Data;

            return data;
        }
    }
}
