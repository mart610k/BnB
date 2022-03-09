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
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT FacilityID, FacilityName FROM facilities JOIN facility_room ON facilities.FacilityID = facility_room.FK_FacilityID JOIN room ON FK_RoomID = room.RoomID where RoomID = @id;";
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

            comm.CommandText = "SELECT * FROM facilities";

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
    }
}
