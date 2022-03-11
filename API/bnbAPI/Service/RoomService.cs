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

            comm.CommandText = "SELECT room.roomID as RoomID,roomAddress,RoomOwner,RoomBriefDescription,Price,if(rent.RoomID IS NULL,false,True) as booked FROM room LEFT JOIN `Rent` ON room.RoomID = Rent.RoomID AND CURDATE() BETWEEN `From` AND `To`;";

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                rooms.Add(new SimpleRoomDTO(reader.GetInt32("RoomID"), reader.GetString("RoomAddress"), reader.GetString("RoomOwner"), reader.GetBoolean("Booked"), reader.GetString("RoomBriefDescription"), reader.GetInt32("Price")));
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
        
        public int CreateRoom(string userID, CreateRoomDTO roomDTO)
        {

            int test = -1;
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = "INSERT INTO Room(RoomOwner,RoomAddress,RoomDescription,RoomBriefDescription,Price) VALUE (@username,@address,@description,@briefDescription,@price); SELECT @@IDENTITY;";
                    comm.Parameters.AddWithValue("@username", userID);
                    comm.Parameters.AddWithValue("@address", roomDTO.Address);
                    comm.Parameters.AddWithValue("@description", roomDTO.Description);
                    comm.Parameters.AddWithValue("@briefDescription", roomDTO.BriefDescription);
                    comm.Parameters.AddWithValue("@price", roomDTO.Price);


                    MySqlDataReader reader = comm.ExecuteReader();
                    while(reader.Read())
                    {
                        test = reader.GetInt32("@@IDENTITY");
                    }
                }
                catch (Exception e)
                {
                    try
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        throw e;
                    }
                    catch
                    {
                        throw e;
                    }
                }

                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return test;
        }

        public DetailedRoomDTO GetDetailedRoom(int id)
        {
            DetailedRoomDTO detailedRoom = null;
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT Room.RoomID as RoomID, RoomAddress, RoomOwner, RoomDescription,Price, if(rent.RoomID IS NULL,false,True) as Booked FROM room LEFT JOIN `Rent` ON room.RoomID = Rent.RoomID AND CURDATE() BETWEEN `From` AND `To` WHERE Room.RoomID = @id";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                detailedRoom = new DetailedRoomDTO(reader.GetString("RoomAddress"), reader.GetString("RoomOwner"), reader.GetInt32("RoomID"), reader.GetString("RoomDescription"),reader.GetBoolean("Booked"), reader.GetInt32("Price"));
            }
            reader.Close();
            conn.Close();
            return detailedRoom;
        }
    }
}
