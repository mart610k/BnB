using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace bnbAPI.Static
{
    public class RegexHelper
    {
        public static bool CheckEmailAddressValidFormat(string email)
        {
            Regex regex = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$",RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}
