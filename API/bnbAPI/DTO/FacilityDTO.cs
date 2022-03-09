using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class FacilityDTO
    {
        public int FacilityID { get; set; }
        public string FacilityName { get; set; }

        public FacilityDTO(int id, string name)
        {
            FacilityID = id;
            FacilityName = name;
        }
    }
}
