using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class RoomService
    {
        public RoomService()
        {

        }
        /// <summary>
        /// Get's a list of rooms in simple details
        /// </summary>
        /// <returns></returns>
        public List<SimpleRoomDTO> GetSimpleRooms()
        {
            List<SimpleRoomDTO> rooms = new List<SimpleRoomDTO>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT RoomID, RoomAddress, RoomOwner, StatusName, RoomBriefDescription FROM room JOIN status_room ON room.RoomID = status_room.FK_RoomID JOIN status ON FK_StatusID = status.StatusID;";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new SimpleRoomDTO(reader.GetInt32("RoomID"), reader.GetString("RoomAddress"), reader.GetString("RoomOwner"), reader.GetString("StatusName"), reader.GetString("RoomBriefDescription")));
            }
            reader.Close();
            conn.Close();
            return rooms;
        }

        public List<SimpleRoomDTO> GetRoomsBySearch(string[] id)
        {
            List<SimpleRoomDTO> rooms = new List<SimpleRoomDTO>();

            for (int i = 0; i < id.Length; i++)
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();

                comm.CommandText = "SELECT RoomID, RoomAddress, RoomOwner, StatusName, RoomBriefDescription FROM room LEFT JOIN status_room ON room.RoomID = status_room.FK_RoomID LEFT JOIN status ON FK_StatusID = status.StatusID Left Join facility_room on room.RoomID = facility_room.FK_RoomID WHERE FK_FacilityID LIKE @id;";
                comm.Parameters.AddWithValue("@id", Convert.ToInt32(id[i]));

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    rooms.Add(new SimpleRoomDTO(reader.GetInt32("RoomID"), reader.GetString("RoomAddress"), reader.GetString("RoomOwner"), reader.GetString("StatusName"), reader.GetString("RoomBriefDescription")));
                }
                reader.Close();
                conn.Close();
            }
            return rooms;
        }

        public DetailedRoomDTO GetDetailedRoom(int id)
        {
            DetailedRoomDTO detailedRoom = null;
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT RoomID, RoomAddress, RoomOwner, RoomDescription, StatusName FROM room JOIN status_room ON room.RoomID = status_room.FK_RoomID JOIN status ON FK_StatusID = status.StatusID WHERE RoomID = @id";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                detailedRoom = new DetailedRoomDTO(reader.GetString("RoomAddress"), reader.GetString("RoomOwner"), reader.GetInt32("RoomID"), reader.GetString("RoomDescription"), reader.GetString("StatusName"));
            }
            reader.Close();
            conn.Close();
            return detailedRoom;
        }
    }
}
