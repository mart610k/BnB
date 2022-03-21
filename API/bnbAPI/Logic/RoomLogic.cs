using bnbAPI.CustomException;
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
        private UserTypeRightsService userrightsService = new UserTypeRightsService();
        
        public RoomLogic()
        {

        }

        /// <summary>
        /// Proxy between Service and API
        /// </summary>
        /// <returns></returns>
        public List<SimpleRoomDTO> GetSimpleRooms()
        {
            List<SimpleRoomDTO> room = roomService.GetSimpleRooms();
            for (int i = 0; i < room.Count; i++)
            {
                SimpleRoomDTO roomDTO = room[i];
                roomDTO.Facilities = facilityService.GetFacilities(room[i].RoomID);
            }
            return room;
        }

        public List<SimpleRoomDTO> GetRoomsBySearch(int[] search)
        {

            List<SimpleRoomDTO> room = roomService.GetRoomsBySearch(search);

            for (int i = 0; i < room.Count; i++)
            {
                SimpleRoomDTO roomDTO = room[i];
                roomDTO.Facilities = facilityService.GetFacilities(room[i].RoomID);
            }

            return room;
        }

        /// <summary>
        /// Gets the Data from a room, in a more detailed view. This includes things such as pictures and facilities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                if (userrightsService.GetIfUserIsHost(userID))
                {
                    int roomID = roomService.CreateRoom(userID, roomDTO);


                    if (roomDTO.Facilities.Count != 0)
                    {
                        facilityService.AddFacilitiesToRoom(roomID, roomDTO.Facilities);
                    }

                    return roomService.GetDetailedRoom(roomID);
                }
                else
                {
                    throw new UserNotAuthorizedForActionException();
                }


            }
            throw new Exception();   
        }

        public void BookRoom(string accesstoken, BookedRoomDTO bookedroomdto)
        {
            string userid = authService.GetUserIDByAccessToken(accesstoken);
            if (userid != null)
            {
                roomService.BookRoom(userid, bookedroomdto);
            }
        }
    }
}
