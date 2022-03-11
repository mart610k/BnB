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

        public List<SimpleRoomDTO> GetRoomsBySearch(string[] search)
        {
            List<SimpleRoomDTO> room = roomService.GetRoomsBySearch(search);

            for (int i = 0; i < room.Count; i++)
            {
                SimpleRoomDTO roomDTO = room[i];
                roomDTO.Facilities = facilityService.GetSearchedFacilities(search);
                room.RemoveAt(i);
                room.Add(roomDTO);
            }

            return room;
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
