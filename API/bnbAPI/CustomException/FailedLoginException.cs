using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.CustomException
{
    public class FailedLoginException : Exception
    {
        public string loginFailed()
        {
            return "Password Doesn't Match";
        }
    }
}
