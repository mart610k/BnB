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

        public List<FacilityDTO> GetSearchedFacilities(string[] facility)
        {
            List<FacilityDTO> facilityDTO = new List<FacilityDTO>();
            
            for (int i = 0; i < facility.Length; i++)
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "Select facilityname, facilityid from facility Join facility_room on facility.facilityid = facility_room.fk_facilityid Where fk_facilityid = @id";
                comm.Parameters.AddWithValue("@id", Convert.ToInt32(facility[i]));

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    facilityDTO.Add(new FacilityDTO(reader.GetInt32("facilityid"), reader.GetString("facilityname")));
                }
                reader.Close();
                conn.Close();
            }
            return facilityDTO;
        }

        /// <summary>
        /// Gets all the facilities, based on the Room's ID. And returns it in the form of a list of facilities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FacilityDTO> GetFacilities(int id)
        {
            List<FacilityDTO> facility = new List<FacilityDTO>();

            using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
            {
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT facilityname, facilityid FROM facility JOIN facility_room ON facility.facilityid = facility_room.fk_facilityid JOIN room ON fk_roomid = room.roomid where roomid = @id;";
                comm.Parameters.AddWithValue("@id", id);

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    facility.Add(new FacilityDTO(reader.GetInt32("facilityid"), reader.GetString("facilityname")));
                }
            }
            return facility;
        }

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

        public void AddFacilitiesToRoom(int roomid, List<int> facilities)
        {
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();
            comm.Parameters.AddWithValue("@roomID", roomid);
            comm.CommandText = "INSERT INTO facility_room(fk_roomid,fk_facilityid) VALUE(@roomid, @facilityid);";
            conn.Open();

            for (int i = 0; i < facilities.Count; i++)
            {
                comm.Parameters.AddWithValue("@facilityid", facilities[i]);
                comm.ExecuteNonQuery();
                comm.Parameters.RemoveAt("@facilityid");
            }

            conn.Close();
        }
    }
}
