using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class CreateRoomDTO
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string BriefDescription { get; set; }

        public int Price { get; set; }

        public List<int> Facilities { get; set; }
    }
}
