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
    public class MessagesService : BaseService
    {

        public MessagesService()
        {
            
        }

        public async Task<PageResponse<MessagesDto>> GetAllMessages(string keyword)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);
            var keywordSearch = "";
            if (!string.IsNullOrEmpty(keyword))
            {
                keywordSearch = "Keyword=" + keyword;
            }
            RestRequest request = new RestRequest("Chat?" + keywordSearch);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userObj.Token}");

            var response = Client.Get<PageResponse<MessagesDto>>(request);

            var data = response.Data;

            return data;
        }
    }
}
