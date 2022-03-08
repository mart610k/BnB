using bnbAPI.DTO;
using bnbAPI.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            string authorization = Request.Headers["authorization"];

            string body;
            using (System.IO.StreamReader str = new System.IO.StreamReader(Request.Body, System.Text.Encoding.UTF8, true, 1024, true))
            {
                body = await str.ReadToEndAsync();

            }

            string[] values = body.Split('&');

            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            temp temp = new temp();
            for (int i = 0; i < values.Length; i++)
            {
                string[] valueskey = values[i].Split('=');
                keyValuePairs.Add(valueskey[0], valueskey[1]);
            }

            if(keyValuePairs.ContainsKey("grant_type") && keyValuePairs["grant_type"] == "password" && authorization.StartsWith("Basic"))
            {
                AccessTokenDTO messageDTO = authLogic.getAccessToken(authorization,keyValuePairs);

                return StatusCode(200, messageDTO);
            }
            else
            {
                return StatusCode(400);
            }
            

            return NotFound();
        }

    }

    public class temp
    {
        public string grant_type;
        public string username;
        public string password;
    }
}
