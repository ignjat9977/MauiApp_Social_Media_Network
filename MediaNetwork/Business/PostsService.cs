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
    public class PostsService : BaseService
    {
        public PostsService()
        {
            
        }

        public async Task<PageResponse<PostDto>> GetSearchedPosts(SearchDto searchDto, int? userId)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);
            var keywordSearch = "";
            int? pageSearch = 1;
            if (userId == null)
            {
                userId = userObj.Id;
            }
            if (!string.IsNullOrEmpty(searchDto.Keyword))
            {
                keywordSearch  = "&Keyword=" + searchDto.Keyword;
            }
            if(searchDto.Page != null)
            {
                pageSearch = searchDto.Page;
            }
            RestRequest request = new RestRequest("Posts?" + "UserId=" + userId + keywordSearch + "&Page=" + pageSearch);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userObj.Token}");

            var response = Client.Get<PageResponse<PostDto>>(request);

            var data = response.Data;

            return data;
        }
        public async Task<bool> UpdatePost(UpdatePostDto postDto)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);

            RestRequest request = new RestRequest("Posts/"+postDto.Id);

            request.AddJsonBody(postDto);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userObj.Token}");
            var response = Client.Put(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreatePost(CreatePostDto postDto)
        {
            var user = await SecureStorage.Default.GetAsync("user");
            var userObj = JsonConvert.DeserializeObject<User>(user);

            RestRequest request = new RestRequest("Posts");

            request.AddJsonBody(postDto);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {userObj.Token}");
            var response = Client.Post(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }
    }
}
