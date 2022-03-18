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
                using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
                {


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
            }
            catch (Exception e)
            {
                throw e;
            }
        return userCredentials;
        }

        public bool UserHaveOutstandingHostRequest(string userID)
        {
            bool toReturn = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
                {


                    conn.Open();

                    MySqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        MySqlCommand comm = conn.CreateCommand();
                        comm.Connection = conn;
                        comm.Transaction = trans;

                        comm.CommandText = "SELECT COUNT(*) FROM userhostrequest WHERE fk_username = @userName AND fk_requeststatus = \"requested\";";
                        comm.Parameters.AddWithValue("@userName", userID);
                        MySqlDataReader reader = comm.ExecuteReader();
                        while (reader.Read())
                        {
                            toReturn = reader.GetInt32("COUNT(*)") != 0;
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
            }
            catch (Exception e)
            {
                throw e;
            }
            return toReturn;
        }


        public void RequestUserAsHost(string userID, RequestHostDTO requestHostDTO)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Config.GetConnectionString()))
                {


                    conn.Open();

                    MySqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        MySqlCommand comm = conn.CreateCommand();
                        comm.Connection = conn;
                        comm.Transaction = trans;

                        comm.CommandText = "INSERT INTO userhostrequest(fk_username,requesttext,fk_requeststatus) VALUE (@userid,@requestText,'requested');";
                        comm.Parameters.AddWithValue("@userid", userID);
                        comm.Parameters.AddWithValue("@requestText", requestHostDTO.RequestText);
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
            }
            catch (Exception e)
            {
                throw e;
            }
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