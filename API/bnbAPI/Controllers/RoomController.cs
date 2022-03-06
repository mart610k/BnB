using bnbAPI.Logic;
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
            return Ok(logic.GetSimpleRoomDTOs());
        }
    }
}
