using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;

namespace bnbAPI.Service
{


    public class UserService
    {
        PasswordHashService passwordHashService = new PasswordHashService();


        public UserCredentialsDTO GetUserCredentials(string userID)
        {
            UserCredentialsDTO userCredentials = null;
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();
                    comm.Connection = conn;
                    comm.Transaction = trans;

                    comm.CommandText = "SELECT UserName,Password FROM UserCredential WHERE UserName = @userName;";
                    comm.Parameters.AddWithValue("@userName", userID);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        userCredentials = new UserCredentialsDTO(reader.GetString("UserName"), reader.GetString("Password"));
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
        return userCredentials;
        }


        public void RegisterUser(RegisterUserDTO registerUser)
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

                    comm.CommandText = "INSERT INTO UserInformation(Username,Email,Name) VALUE (@username,@email,@name);";
                    comm.Parameters.AddWithValue("@username", registerUser.Username);
                    comm.Parameters.AddWithValue("@email", registerUser.Email);
                    comm.Parameters.AddWithValue("@name", registerUser.Name);
                    comm.ExecuteNonQuery();
                    comm.CommandText = "INSERT INTO UserCredential(Username,Password) VALUE (@username,@password);";
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
        }
    }
}