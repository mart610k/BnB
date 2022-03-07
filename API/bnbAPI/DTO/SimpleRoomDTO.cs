using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class SimpleRoomDTO
    {
        public string RoomAddress { get; set; }
        public string RoomOwner { get; set; }
        public string RoomStatus { get; set; }
        public int RoomID { get; set; }
        public string RoomDesc { get; set; }
        public List<string> RoomPicture { get; set; }

        public SimpleRoomDTO()
        {

        }

        public SimpleRoomDTO(int id,string address, string owner, string status, string desc)
        {
            RoomID = id;
            RoomAddress = address;
            RoomOwner = owner;
            RoomStatus = status;
            RoomDesc = desc;
            RoomPicture = new List<string>();
        }
    }
}
