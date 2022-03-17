using bnbAPI.DTO;
using bnbAPI.Logic;
using bnbAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthLogic authLogic = new AuthLogic();



        [HttpPost("token")]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                string authorization = Request.Headers["authorization"];

                string body;
                using (System.IO.StreamReader str = new System.IO.StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await str.ReadToEndAsync();

                }

                string[] values = body.Split('&');

                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                for (int i = 0; i < values.Length; i++)
                {
                    string[] valueskey = values[i].Split('=');
                    keyValuePairs.Add(valueskey[0], valueskey[1]);
                }

                if (keyValuePairs.ContainsKey("grant_type") && authorization.StartsWith("Basic"))
                {
                    string authBase64 = authorization.Substring(6);

                    string clientKeySecret = Encoding.UTF8.GetString(Convert.FromBase64String(authBase64));

                    string[] array = clientKeySecret.Split(':');

                    if (keyValuePairs["grant_type"] == "password")
                    {
                        AccessTokenAuthorizationDTO accessTokenAuthorizationDTO = new AccessTokenAuthorizationDTO(keyValuePairs["username"], keyValuePairs["password"], array[0], array[1]);

                        AccessTokenDTO messageDTO = authLogic.getAccessToken(accessTokenAuthorizationDTO);

                        return StatusCode(200, messageDTO);

                    }
                    else if (keyValuePairs["grant_type"] == "refresh_token") {
                        UpdateAccessTokenDTO updateAccessTokenDTO = new UpdateAccessTokenDTO(keyValuePairs["refresh_token"], array[0], array[1]);

                        AccessTokenDTO accessToken = authLogic.UpdateAccessToken(updateAccessTokenDTO);
                        return StatusCode(200, accessToken);
                    }
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch(Exception e)
            {
                MessageDTO messageDTO = HttpStatusCodeService.GetMessageDTOFromException(e);

                return StatusCode(messageDTO.StatusCode, messageDTO);
            }

            
            return NotFound();
        }


        [HttpPost("logout")]
        public IActionResult LogoutUser()
        {
            string authorization = Request.Headers["authorization"];
            string authBase64 = authorization.Substring(7);

            if (authLogic.LogoutUser(authBase64))
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(400);
            }
        }
    }
}
