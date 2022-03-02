using bnbAPI.DTO;
using bnbAPI.Logic;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(userLogic.RegisterUser(registerUser));
        }

    }
}
