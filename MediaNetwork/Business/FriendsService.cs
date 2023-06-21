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
    internal class FriendsService : BaseService
    {

        public FriendsService()
        {
            
        }

        public async Task<FriendsAndSuggestedFriens> GetFriendsAndSuggestedFriends(int? id)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);
            if (id == null)
            {
                id = userObj.Id;
            }
            RestRequest request = new RestRequest("Friend?Id="+ id);
            request.AddHeader("Content-Type", "application/json");
            string token = await SecureStorage.Default.GetAsync("jwt");
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = Client.Get<FriendsAndSuggestedFriens>(request);

            var data = response.Data;

            return data;

        }
        public async Task<bool> DeleteFriend(UserDto friend)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);
            RestRequest request = new RestRequest("Friend/"+friend.Id);
            request.AddHeader("Content-Type", "application/json");
            string token = await SecureStorage.Default.GetAsync("jwt");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = Client.Delete(request);

            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
        public async Task<bool> AddFriend(AddFriendDto dto)
        {
            var user = await SecureStorage.Default.GetUser();

            RestRequest request = new RestRequest("Friend");
            dto.UserId = user.Id;
            request.AddJsonBody(dto);
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            var response = Client.Post(request);

            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }



            return false;
        }
    }
}
