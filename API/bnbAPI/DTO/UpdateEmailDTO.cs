using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class UpdateEmailDTO
    {
        public string UserID { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }

        public UpdateEmailDTO()
        {

        }

        public UpdateEmailDTO(string userID, string oldEmail, string newEmail)
        {
            UserID = userID;
            OldEmail = oldEmail;
            NewEmail = newEmail;
        }
    }
}
