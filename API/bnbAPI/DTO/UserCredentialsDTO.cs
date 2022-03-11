using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.DTO
{
    public class UserCredentialsDTO
    {
        public string UserName { get; set; }

        public string PasswordHashed { get; set; }


        public UserCredentialsDTO()
        {

        }

        public UserCredentialsDTO(string username, string passwordHashed)
        {
            UserName = username;
            PasswordHashed = passwordHashed;
        }

    }
}
