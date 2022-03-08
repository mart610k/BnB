using bnbAPI.DTO;
using bnbAPI.Service;
using bnbAPI.Static;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bnbAPI.Logic
{

    public class AuthLogic
    {
        UserService userService = new UserService();
        PasswordHashService passwordHashService = new PasswordHashService();
        AuthService authService = new AuthService();


        public AccessTokenDTO getAccessToken(string authorization, Dictionary<string, string> keyValuePairs)
        {

            string authBase64 = authorization.Substring(6);


            string clientKeySecret = Encoding.UTF8.GetString(Convert.FromBase64String(authBase64));

            string[] array = clientKeySecret.Split(':');

            if (Config.ClientKey == array[0] && Config.ClientSecret == array[1])
            {
                UserCredentialsDTO credentials = userService.GetUserCredentials(keyValuePairs["username"]);

                if (credentials == null)
                {
                    throw new Exception();
                }

                if (passwordHashService.Verify(keyValuePairs["password"], credentials.PasswordHashed))
                {
                    AccessTokenDTO accessToken = new AccessTokenDTO();
                    try
                    {
                        AccessTokenDTO token = authService.GetAccessTokenByUserID(keyValuePairs["username"]);
                        if(token.Expires_in < 200)
                        {
                            authService.UpdateAccessTokenForUser(keyValuePairs["username"]);

                        }

                    }
                    catch (Exception e)
                    {
                        authService.RegisterAccessToken(keyValuePairs["username"]);

                    }
                    accessToken = authService.GetAccessTokenByUserID(keyValuePairs["username"]);

                    return accessToken;
                }
                else
                {
                    throw new Exception();
                }

            }
            else
            {
                throw new Exception();
            }

            throw new Exception();
        
        }

    }
}
