using bnbAPI.DTO;
using bnbAPI.Service;
using System;
using System.Collections.Generic;

namespace bnbAPI.Logic
{
    public class RoomLogic
    {
        private RoomService roomService = new RoomService();
        private FacilityService facilityService = new FacilityService();
        private PictureService pictureService = new PictureService();
        private AuthService authService = new AuthService();
        private RentSercixe sercixe = new RentSercixe();
        
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

        public List<SimpleRoomDTO> GetRoomsBySearch(int[] search)
        {

            List<SimpleRoomDTO> room = roomService.GetRoomsBySearch(search);
            //List<SimpleRoomDTO> room = roomService.GetRoomsBySearch(search);

            for (int i = 0; i < room.Count; i++)
            {
                SimpleRoomDTO roomDTO = room[i];
                roomDTO.Facilities = facilityService.GetFacilities(room[i].RoomID);
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

        public DetailedRoomDTO CreateRoom(string accessToken, CreateRoomDTO roomDTO)
        {
            string userID = authService.GetUserIDByAccessToken(accessToken);
            if(userID != null)
            {

                int roomID = roomService.CreateRoom(userID, roomDTO);


                if (roomDTO.Facilities.Count != 0)
                {
                    facilityService.AddFacilitiesToRoom(roomID, roomDTO.Facilities);
                }

                return roomService.GetDetailedRoom(roomID);


            }
            throw new Exception();
            
        }
    }
}
