using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class RentSercixe
    {

        public bool CheckRoomAvaibilityForToday(int roomID)
        {
            bool notbooked = true;
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();
                conn.Open();

                comm.CommandText = "SELECT `From`, `To`, `Accepted` FROM `Rent` WHERE `RoomId` = @roomID AND CURDATE() between `From` AND `To`;";

                comm.Parameters.AddWithValue("@roomID", roomID);

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
