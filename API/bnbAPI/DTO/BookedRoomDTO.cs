using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class BookedRoomDTO
    {
        public int RoomID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookedRoomDTO()
        {

        }

        public BookedRoomDTO(int id, DateTime startdate, DateTime enddate)
        {
            RoomID = id;
            StartDate = startdate;
            EndDate = enddate;
        }
    }
}
