using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class UpdateAccessTokenDTO
    {
        public string RefreshToken { get; set; }


        public string ClientKey { get; set; }

        public string ClientSecret { get; set; }


        public UpdateAccessTokenDTO(string refreshToken, string clientKey, string clientSecret)
        {
            RefreshToken = refreshToken;
            ClientKey = clientKey;
            ClientSecret = clientSecret;
        }
    }
}
