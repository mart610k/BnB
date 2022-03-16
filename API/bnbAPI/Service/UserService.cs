using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;

namespace bnbAPI.Service
{


    public class UserService
    {
        PasswordHashService passwordHashService = new PasswordHashService();


        public UserCredentialsDTO GetUserCredentials(string userid)
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

                    comm.CommandText = "SELECT username,password FROM usercredential WHERE username = @userName;";
                    comm.Parameters.AddWithValue("@userName", userid);
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        userCredentials = new UserCredentialsDTO(reader.GetString("username"), reader.GetString("password"));
                    }
                    reader.Close();
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


        public void RegisterUser(RegisterUserDTO registeruser)
        {
            try
            {
                string hashedPassword = passwordHashService.Hash(registeruser.Password);

                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                MySqlTransaction trans = conn.BeginTransaction();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();
                    comm.Connection = conn;
                    comm.Transaction = trans;

                    comm.CommandText = "INSERT INTO userinformation(username,email,name) VALUE (@username,@email,@name);";
                    comm.Parameters.AddWithValue("@username", registeruser.Username);
                    comm.Parameters.AddWithValue("@email", registeruser.Email);
                    comm.Parameters.AddWithValue("@name", registeruser.Name);
                    comm.ExecuteNonQuery();
                    comm.CommandText = "INSERT INTO usercredential(username,password) VALUE (@username,@password);";
                    comm.Parameters.AddWithValue("@password", hashedPassword);
                    comm.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (Exception e)
                {
                    try
                    {
                        trans.Rollback();
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
        }

        public void UpdateUserPassword(UpdatePassDTO updatePass)
        {
            try
            {
                string oldHashedPassword = passwordHashService.Hash(updatePass.OldPass);


                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    if (passwordHashService.Verify(updatePass.OldPass, GetUserCredentials(updatePass.UserID).PasswordHashed))
                    {
                        if (updatePass.NewPass != null)
                        {

                            string newHashedPassword = passwordHashService.Hash(updatePass.NewPass);
                            MySqlCommand comm = conn.CreateCommand();

                            comm.CommandText = "UPDATE usercredential SET password = @pass WHERE username = @username;";
                            comm.Parameters.AddWithValue("@pass", newHashedPassword);
                            comm.Parameters.AddWithValue("@username", updatePass.UserID);

                            comm.ExecuteNonQuery();
                        }
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
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public void UpdateEmail(UpdateEmailDTO updateEmail)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    if (updateEmail.NewEmail != null)
                    {

                        MySqlCommand comm = conn.CreateCommand();

                        comm.CommandText = "UPDATE userinformation SET email = @email WHERE username = @username;";
                        comm.Parameters.AddWithValue("@email", updateEmail.NewEmail);
                        comm.Parameters.AddWithValue("@username", updateEmail.UserID);

                        comm.ExecuteNonQuery();
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
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}