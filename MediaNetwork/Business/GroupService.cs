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
    public class GroupService : BaseService
    {

        public GroupService()
        {
            
        }

        public async Task<PageResponse<GroupDto>> GetAllGroups(string keyword)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);
            var keywordSearch = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                keywordSearch = "Keyword=" + keyword;
            }
            RestRequest request = new RestRequest("Group?" + keywordSearch);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userObj.Token}");

            var response = Client.Get<PageResponse<GroupDto>>(request);

            var data = response.Data;

            return data;
        }
    }
}
