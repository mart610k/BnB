using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class DetailedRoomDTO
    {
        public string RoomAddress { get; set; }
        public string RoomOwner { get; set; }
        //public string RoomStatus { get; set; }
        public int RoomID { get; set; }
        public string RoomDesc { get; set; }
        public List<string> RoomPictures { get; set; }
        public List<FacilityDTO> RoomFacilities { get; set; }
        public bool Booked { get; set; }
        public int Price { get; set; }
        public string RoomStatus { get; set; }

        public DetailedRoomDTO(string address, string owner, int id, string desc, List<string> pictures, List<FacilityDTO> facilities, string status)
        {
            RoomAddress = address;
            RoomDesc = desc;
            RoomFacilities = facilities;
            RoomID = id;
            RoomPictures = pictures;
            RoomOwner = owner;
            RoomStatus = status;
        }
        public DetailedRoomDTO()
        {
            RoomPictures = new List<string>();
            RoomFacilities = new List<FacilityDTO>();
        }

        public DetailedRoomDTO(string address, string owner, int id, string desc, bool booked, int price) : this()
        {
            RoomAddress = address;
            RoomDesc = desc;
            RoomID = id;
            RoomOwner = owner;
            Booked = booked;
            Price = price;
        }

        public DetailedRoomDTO(string address, string owner, int id, string desc, List<string> pictures, List<string> facilities, bool booked, int price) : this(address,owner,id,desc,booked,price)
        {
            RoomFacilities = facilities;
            RoomPictures = pictures;
            
        }
    }
}
