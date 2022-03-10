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

        public List<string> GetFacilities(int id)
        {
            List<string> facility = new List<string>();
            try
            {

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

            comm.CommandText = "SELECT FacilityID,FacilityName FROM facilities";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                facility.Add(new FacilityDTO(reader.GetInt32("FacilityID"),reader.GetString("FacilityName")));
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
            comm.CommandText = "INSERT INTO Facility_Room(FK_RoomID,FK_FacilityID) VALUE(@roomID, @facilityID);";
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
