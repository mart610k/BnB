using bnbAPI.DTO;
using bnbAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Logic
{
    public class RoomLogic
    {
        private RoomService roomService = new RoomService();
        public RoomLogic()
        {

        }

        /// <summary>
        /// Proxy between Service and API
        /// </summary>
        /// <returns></returns>
        public List<SimpleRoomDTO> GetSimpleRoomDTOs()
        {
            return roomService.GetSimpleRooms();
        }
    }
}
