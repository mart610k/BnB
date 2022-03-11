using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Static
{
    public class AuthorizationHelper
    {

        public static string GetAccessTokenFromBearerHeader(string authHeader)
        {
            return authHeader.Substring(7);
        }
    }
}
