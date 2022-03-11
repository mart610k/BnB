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
                comm.CommandText = "Select FacilityName, FacilityID from facility Join facility_room on facility.FacilityID = facility_room.FK_FacilityID Where FK_FacilityID = @id";
                comm.Parameters.AddWithValue("@id", Convert.ToInt32(facility[i]));

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    facilityDTO.Add(new FacilityDTO(reader.GetInt32("FacilityID"), reader.GetString("FacilityName")));
                }
                reader.Close();
                conn.Close();
            }
            return facilityDTO;
        }
        /*
         SELECT DISTINCT RoomID, RoomAddress, RoomOwner, StatusName, RoomBriefDescription, FROM room JOIN status_room ON room.RoomID = status_room.FK_RoomID JOIN status ON FK_StatusID = status.StatusID JOIN facility_room ON facility_room.FK_RoomID = room.RoomID JOIN facility on facility_room.FK_FacilityID = facility.FacilityID WHERE facility_room.FK_FacilityID = 3 OR facility_room.FK_FacilityID = 4; 
         */
        public List<FacilityDTO> GetFacilities(int id)
        {
            List<FacilityDTO> facility = new List<FacilityDTO>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT FacilityID, FacilityName FROM facility JOIN facility_room ON facility.FacilityID = facility_room.FK_FacilityID JOIN room ON FK_RoomID = room.RoomID where RoomID = @id;";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                facility.Add(new FacilityDTO(reader.GetInt32("FacilityID"), reader.GetString("FacilityName")));
            }
            reader.Close();
            conn.Close();
            return facility;
        }

        public List<FacilityDTO> GetAllFacilities()
        {
            List<FacilityDTO> facility = new List<FacilityDTO>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT * FROM facility";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                facility.Add(new FacilityDTO(reader.GetInt32("FacilityID"), reader.GetString("FacilityName")));
            }
            reader.Close();
            conn.Close();
            return facility;
        }
    }
}
