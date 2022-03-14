using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
            {


                MySqlCommand comm = conn.CreateCommand();

                comm.CommandText = "SELECT roomid,address,owner,briefdescription,price,if(rent.fk_roomid IS NULL,false,True) as booked FROM room LEFT JOIN `rent` ON room.roomid = rent.fk_roomid AND CURDATE() BETWEEN `from` AND `to`;";

                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    rooms.Add(new SimpleRoomDTO(reader.GetInt32("roomid"), reader.GetString("address"), reader.GetString("owner"), reader.GetBoolean("booked"), reader.GetString("briefdescription"), reader.GetInt32("price")));
                }
            }
            return rooms;
        }

        public List<SimpleRoomDTO> GetRoomsBySearch(int[] id)
        {
            List<SimpleRoomDTO> rooms = new List<SimpleRoomDTO>();

            using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
            {
                MySqlCommand comm = conn.CreateCommand();


                StringBuilder sb = new StringBuilder();


                for (int i = 0; i < id.Length; i++)
                {
                    string format = string.Format("@p{0}", id[i]);


                    sb.AppendFormat(format + ",", id[i]);

                    comm.Parameters.AddWithValue(format, id[i]);
                }

                if(sb.Length != 0){
                    sb.Remove(sb.Length - 1, 1);
                    sb.Insert(0, "WHERE fk_facilityid in (");
                    sb.Append(") ");
                }

                comm.CommandText = "SELECT count(roomid), roomid,address,owner,briefdescription,price,if(rent.fk_roomid IS NULL,false,True) as booked FROM room LEFT JOIN `rent` ON room.roomid = rent.fk_roomid AND CURDATE() BETWEEN `from` AND `to` Left Join facility_room on room.roomid = facility_room.fk_roomid "+  sb.ToString() +"group by roomid;";


                conn.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32("count(roomid)") >= id.Length)
                    {
                        rooms.Add(new SimpleRoomDTO(reader.GetInt32("roomid"), reader.GetString("address"), reader.GetString("owner"), reader.GetBoolean("booked"), reader.GetString("briefdescription"), reader.GetInt32("price")));
                    }
                }
                reader.Close();
                conn.Close();
            }
            return rooms;
        }

        public int CreateRoom(string userid, CreateRoomDTO roomdto)
        {

            int test = -1;
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = "INSERT INTO Room(owner,address,description,briefdescription,price) VALUE (@username,@address,@description,@briefDescription,@price); SELECT @@IDENTITY;";
                    comm.Parameters.AddWithValue("@username", userid);
                    comm.Parameters.AddWithValue("@address", roomdto.Address);
                    comm.Parameters.AddWithValue("@description", roomdto.Description);
                    comm.Parameters.AddWithValue("@briefDescription", roomdto.BriefDescription);
                    comm.Parameters.AddWithValue("@price", roomdto.Price);


                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
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

            comm.CommandText = "SELECT roomid, address, owner, description,price, if(rent.fk_roomid IS NULL,false,True) as booked FROM room LEFT JOIN `rent` ON room.roomid = rent.fk_roomid AND CURDATE() BETWEEN `from` AND `to` WHERE roomid = @id";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                detailedRoom = new DetailedRoomDTO(reader.GetString("address"), reader.GetString("owner"), reader.GetInt32("roomid"), reader.GetString("description"), reader.GetBoolean("booked"), reader.GetInt32("price"));
            }
            reader.Close();
            conn.Close();
            return detailedRoom;
        }
    }
}
