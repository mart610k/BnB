using bnbAPI.DTO;
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

        public List<FacilityDTO> GetFacilities(int id)
        {
            List<FacilityDTO> facility = new List<FacilityDTO>();
            try
            {

                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();

                comm.CommandText = "SELECT facilityid,facilityname FROM facility JOIN facility_room ON facility.facilityid = facility_room.fk_facilityid JOIN room ON fk_roomid = room.roomid where roomid = @id;";
                comm.Parameters.AddWithValue("@id", id);

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    facility.Add(new FacilityDTO(reader.GetInt32("facilityid"),reader.GetString("facilityname")));
                }

                reader.Close();
                conn.Close();
            }
            catch
            {

            }
            return facility;
        }

        /// <summary>
        /// Gets all Facilities Present on the Database.
        /// </summary>
        /// <returns></returns>
        public List<FacilityDTO> GetAllFacilities()
        {
            List<FacilityDTO> facility = new List<FacilityDTO>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT facilityid,facilityname FROM facility";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                facility.Add(new FacilityDTO(reader.GetInt32("facilityid"),reader.GetString("facilityname")));
            }
            reader.Close();
            conn.Close();
            return facility;
        }

        public void AddFacilitiesToRoom(int roomID, List<int> facilities)
        {
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();
            comm.Parameters.AddWithValue("@roomID", roomID);
            comm.CommandText = "INSERT INTO facility_room(fk_roomid,fk_facilityid) VALUE(@roomID, @facilityID);";
            conn.Open();

            for (int i = 0; i < facilities.Count; i++)
            {
                comm.Parameters.AddWithValue("@facilityID", facilities[i]);
                comm.ExecuteNonQuery();
                comm.Parameters.RemoveAt("@facilityID");
            }

            conn.Close();
        }
    }
}
