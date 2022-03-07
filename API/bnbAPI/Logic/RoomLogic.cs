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
        private FacilityService facilityService = new FacilityService();
        private PictureService pictureService = new PictureService();
        public RoomLogic()
        {

        }

        /// <summary>
        /// Proxy between Service and API
        /// </summary>
        /// <returns></returns>
        public List<SimpleRoomDTO> GetSimpleRooms()
        {
            return roomService.GetSimpleRooms();
        }

        public DetailedRoomDTO GetDetailedRoom(int id)
        {
            DetailedRoomDTO detailedRoom = roomService.GetDetailedRoom(id);
            detailedRoom.RoomFacilities = facilityService.GetFacilities(id);
            detailedRoom.RoomPictures = pictureService.GetPictureList(id);
            return detailedRoom;
        }
    }
}
