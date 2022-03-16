using bnbAPI.DTO;
using bnbAPI.Logic;
using bnbAPI.Service;
using bnbAPI.Static;
using Microsoft.AspNetCore.Mvc;

namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserLogic userLogic = new UserLogic();
        AuthService authService = new AuthService();

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO registerUser)
        {
            MessageDTO message = userLogic.RegisterUser(registerUser);
            return StatusCode(message.StatusCode,message);
        }

        [HttpPost("UpdatePass")]
        public IActionResult UpdatePassword([FromBody] UpdatePassDTO updatePass, [FromHeader] string authorization)
        {
            string token = AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization);
            string userid = authService.GetUserIDByAccessToken(token);
            updatePass.UserID = userid;

            userLogic.UpdatePassword(updatePass);

            return Ok();
        }

        [HttpPost("UpdateEmail")]
        public IActionResult UpdateEmail([FromBody] UpdateEmailDTO updateEmail, [FromHeader] string authorization)
        {
            string token = AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization);
            string userid = authService.GetUserIDByAccessToken(token);
            updateEmail.UserID = userid;

            userLogic.UpdateEmail(updateEmail);

            return Ok();
        }
    }
}
