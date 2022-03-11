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
        public bool Booked { get; set; }
        public int RoomID { get; set; }
        public string RoomDesc { get; set; }
        public List<string> RoomPicture { get; set; }
        public List<FacilityDTO> Facilities { get; set; }

        public int Price { get; set; }

        public SimpleRoomDTO()
        {

        }

        public SimpleRoomDTO(int id,string address, string owner, bool status, string desc,int price)
        {
            RoomID = id;
            RoomAddress = address;
            RoomOwner = owner;
            Booked = status;
            RoomDesc = desc;
            Price = price;
            RoomPicture = new List<string>();
            Facilities = new List<FacilityDTO>();
        }
    }
}
