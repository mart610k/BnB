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

        public List<SimpleRoomDTO> GetSimpleRooms()
        {
            List<SimpleRoomDTO> rooms = new List<SimpleRoomDTO>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "Select RoomID, RoomAddress, RoomOwner, StatusName, RoomBriefDescription from room inner join status_room on room.RoomID = status_room.FK_RoomID join status on FK_StatusID = status.StatusID;";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while(reader.Read())
            {
                rooms.Add(new SimpleRoomDTO(reader.GetInt32("RoomID"),reader.GetString("RoomAddress"),reader.GetString("RoomOwner"),reader.GetString("StatusName"),reader.GetString("RoomBriefDescription")));
            }
            return rooms;
        }
    }
}
