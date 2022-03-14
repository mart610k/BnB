using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class RentService
    {

        public bool CheckRoomAvaibilityForToday(int roomid)
        {
            bool notbooked = true;
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();
                conn.Open();

                comm.CommandText = "SELECT `from`, `to`, `accepted` FROM `rent` WHERE `roomid` = @roomid AND CURDATE() between `from` AND `to`;";

                comm.Parameters.AddWithValue("@roomid", roomid);

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    notbooked = false;
                }

                conn.Close();

            }
            catch (Exception e)
            {
                throw e;
            }
            return notbooked;
        }
    }
}
