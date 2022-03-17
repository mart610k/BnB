using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bnbAPI.Static;
using MySql.Data.MySqlClient;

namespace bnbAPI.Service
{
    public class UserTypeRightsService
    {

        public bool GetIfUserIsHost(string username)
        {
            bool result = false;

            using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
            {
                MySqlCommand comm = conn.CreateCommand();

                comm.CommandText = "SELECT COUNT(*) FROM `usertypeinformation` WHERE `fk_username` = @username AND `fk_usertype` = \"host\"";
                
                
                comm.Parameters.AddWithValue("@username", username);

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                     result = reader.GetInt32("COUNT(*)") == 0 ? false : true; 
                }

            }
            return result;
        }

    }
}
