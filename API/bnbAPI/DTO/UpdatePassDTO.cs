using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class UpdatePassDTO
    {
        public string UserID { get; set; }
        public string OldPass { get; set; }
        public string NewPass { get; set; }

        public UpdatePassDTO()
        {

        }

        public UpdatePassDTO(string userID, string oldPass, string newPass)
        {
            UserID = userID;
            OldPass = oldPass;
            NewPass = newPass;
        }
    }
}
