using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bnbAPI.Service
{
    public class PictureService
    {
        public PictureService()
        {

        }

        public List<string> GetPictureList(int id)
        {
            List<string> picture = new List<string>();
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT FileName FROM picture JOIN room ON room.RoomID = picture.FK_RoomID where RoomID = @id;";
            comm.Parameters.AddWithValue("@id", id);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                picture.Add(reader.GetString("FileName"));
            }
            reader.Close();
            conn.Close();
            return picture;
        }
    }
}
