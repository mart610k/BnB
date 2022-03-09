using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class AccessTokenAuthorizationDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ClientKey { get; set; }

        public string ClientSecret { get; set; }


        public AccessTokenAuthorizationDTO(string username,string password, string clientKey, string clientSecret)
        {
            Username = username;
            Password = password;
            ClientKey = clientKey;
            ClientSecret = clientSecret;
        }
    }
}
