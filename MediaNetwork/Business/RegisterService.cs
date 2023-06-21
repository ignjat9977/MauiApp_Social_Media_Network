using MediaNetwork.Business.Dto;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaNetwork.Business
{
    public class RegisterService : BaseService
    {
        public RegisterService()
        {
            
        }
        public bool Register(RegisterDto dto)
        {
            RestRequest request = new RestRequest("Register");

            request.AddJsonBody(dto);
            request.AddHeader("Content-Type", "application/json");
            var response = Client.Post(request);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }
    }
}
