using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class FacilityService
    {

        public FacilityService()
        {

        }

        public List<string> GetFacilities(int id)
        {
            List<string> facility = new List<string>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT FacilityName FROM facilities JOIN facility_room ON facilities.FacilityID = facility_room.FK_FacilityID JOIN room ON FK_RoomID = room.RoomID where RoomID = @id;";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                facility.Add(reader.GetString("FacilityName"));
            }
            conn.Close();
            return facility;
        }
    }
}
