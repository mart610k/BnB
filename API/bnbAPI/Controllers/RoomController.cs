using bnbAPI.DTO;
using bnbAPI.Logic;
using bnbAPI.Static;
using Microsoft.AspNetCore.Mvc;


namespace bnbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomLogic logic = new RoomLogic();
        [HttpGet("list")]
        public IActionResult GetSimpleRooms()
        {
            return Ok(logic.GetSimpleRooms());
        }

        [HttpGet("room")]
        public IActionResult GetDetailedRoom([FromQuery] int id)
        {
            return Ok(logic.GetDetailedRoom(id));
        }

        [HttpPost("create")]
        public IActionResult CreateRoom([FromHeader] string authorization, [FromBody] CreateRoomDTO roomDTO)
        {
            return StatusCode(200, logic.CreateRoom(AuthorizationHelper.GetAccessTokenFromBearerHeader(authorization),roomDTO));
        }
    }
}
