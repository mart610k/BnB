using bnbAPI.DTO;
using bnbAPI.Logic;
using bnbAPI.Service;
using bnbAPI.Static;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserLogic userLogic = new UserLogic();

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO registerUser)
        {
            MessageDTO message = userLogic.RegisterUser(registerUser);
            return StatusCode(message.StatusCode,message);
        }



        [HttpPost("request/host")]
        public IActionResult RequestAsHost([FromHeader] string authorization, [FromBody] RequestHostDTO requestHostDTO)
        {
            try
            {
                userLogic.RequestUserAsHost(AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization),requestHostDTO);

                return StatusCode(200);
            }
            catch(Exception e)
            {
                MessageDTO messageDTO = HttpStatusCodeService.GetMessageDTOFromException(e);

                return StatusCode(messageDTO.StatusCode, messageDTO);
            }
        }
    }
}
