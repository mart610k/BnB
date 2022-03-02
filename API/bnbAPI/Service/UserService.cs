using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;

namespace bnbAPI.Service
{


    public class UserService
    {
        PasswordHashService passwordHashService = new PasswordHashService();

        public bool RegisterUser(RegisterUserDTO registerUser)
        {
            try
            {
                string hashedPassword = passwordHashService.Hash(registerUser.Password);
                
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();
                    comm.Connection = conn;
                    comm.Transaction = trans;

                    comm.CommandText = "INSERT INTO UserInformation(Email,Name) VALUE (@email,@name);";
                    comm.Parameters.AddWithValue("@email", registerUser.Email);
                    comm.Parameters.AddWithValue("@name", registerUser.Name);
                    comm.ExecuteNonQuery();
                    comm.CommandText = "INSERT INTO UserCredentials(UserEmail,Password) VALUE (@email,@password);";
                    comm.Parameters.AddWithValue("@password", hashedPassword);
                    comm.ExecuteNonQuery();
                    trans.Commit();

                }
                catch(Exception e)
                {
                    try
                    {
                        trans.Rollback();
                        if(conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                        return false;

                    }
                    catch
                    {
                        return false;
                    }
                }

                conn.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}