using bnbAPI.DTO;
using bnbAPI.Static;
using MySql.Data.MySqlClient;
using System;

namespace bnbAPI.Service
{
    public class AuthService
    {

        public void RegisterAccessToken(string userid)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = "INSERT INTO Oauth2(Username,AccessToken,RefreshToken,Expires) VALUE (@username,UUID_TO_BIN(@accessToken),UUID_TO_BIN(@refreshToken),@expiry);";
                    comm.Parameters.AddWithValue("@username", userid);
                    comm.Parameters.AddWithValue("@accessToken", Guid.NewGuid().ToString());
                    comm.Parameters.AddWithValue("@refreshToken", Guid.NewGuid().ToString());

                    DateTime dateTime = DateTime.UtcNow;
                    dateTime.AddSeconds(Config.AccessTokenValidity);

                    comm.Parameters.AddWithValue("@expiry", dateTime);
                    
                    comm.ExecuteNonQuery();
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
        }

        public void UpdateAccessTokenForUser(string userID)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = "UPDATE Oauth2 SET AccessToken = UUID_TO_BIN(@accessToken),Expires = @expiry WHERE UserName = @username;";
                    comm.Parameters.AddWithValue("@username", userID);
                    comm.Parameters.AddWithValue("@accessToken", Guid.NewGuid().ToString());

                    DateTime dateTime = DateTime.UtcNow;
                    dateTime = dateTime.AddSeconds(Config.AccessTokenValidity);

                    comm.Parameters.AddWithValue("@expiry", dateTime);

                    comm.ExecuteNonQuery();
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
        }

        public string GetUserIDByAccessToken(string accesstoken)
        {
            string userID = null;


            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT UserName FROM Oauth2 WHERE AccessToken = UUID_TO_BIN(@accesstoken)";
            comm.Parameters.AddWithValue("@accesstoken", accesstoken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                userID = reader.GetString("UserName");
            }
            reader.Close();
            conn.Close();

            if (userID == null)
            {
                throw new Exception();
            }
            return userID;
        }

        public string GetUserIDByRefreshToken(string refreshToken)
        {
            string userID = null;


            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT UserName FROM Oauth2 WHERE RefreshToken = UUID_TO_BIN(@refreshToken)";
            comm.Parameters.AddWithValue("@refreshToken", refreshToken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                userID = reader.GetString("UserName");
            }
            reader.Close();
            conn.Close();

            if (userID == null)
            {
                throw new Exception();
            }
            return userID;
        }

        public AccessTokenDTO GetAccessTokenByUserID(string userID)
        {
            AccessTokenDTO accessTokenDTO = null;

            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT BIN_TO_UUID(AccessToken),BIN_TO_UUID(RefreshToken),TIMESTAMPDIFF(SECOND,UTC_TIMESTAMP(), Expires) as ExpiresIn FROM Oauth2 WHERE UserName = @userID";
            comm.Parameters.AddWithValue("@userID", userID);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                accessTokenDTO = new AccessTokenDTO();

                accessTokenDTO.Access_token =  reader.GetString("BIN_TO_UUID(AccessToken)");
                accessTokenDTO.Refresh_token= reader.GetString("BIN_TO_UUID(RefreshToken)");
                accessTokenDTO.Expires_in = reader.GetInt32("ExpiresIn");
                accessTokenDTO.Token_type = "bearer";

            }
            reader.Close();
            conn.Close();

            if (accessTokenDTO == null)
            {
                throw new Exception();
            }
            return accessTokenDTO;
        }

        public AccessTokenDTO GetAccessTokenByAccessToken(string accessToken)
        {
            AccessTokenDTO accessTokenDTO = null;

            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT BIN_TO_UUID(AccessToken),BIN_TO_UUID(RefreshToken),TIMESTAMPDIFF(SECOND,UTC_TIMESTAMP(), Expires) as ExpiresIn FROM Oauth2 WHERE AccessToken = UUID_TO_BIN(@accesstoken)";
            comm.Parameters.AddWithValue("@accesstoken", accessToken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                accessTokenDTO = new AccessTokenDTO();

                accessTokenDTO.Access_token = reader.GetString("BIN_TO_UUID(AccessToken)");
                accessTokenDTO.Refresh_token = reader.GetString("BIN_TO_UUID(RefreshToken)");
                accessTokenDTO.Expires_in = reader.GetInt32("ExpiresIn");
                accessTokenDTO.Token_type = "bearer";

            }
            reader.Close();
            conn.Close();

            if (accessTokenDTO == null)
            {
                throw new Exception();
            }
            return accessTokenDTO;
        }

        public void DeleteAccessTokenEntryByUserID(string userID)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                MySqlCommand comm = conn.CreateCommand();

                comm.CommandText = "DELETE FROM Oauth2 WHERE UserName = @UserName";
                comm.Parameters.AddWithValue("@UserName", userID);

                conn.Open();

                comm.ExecuteNonQuery();

                conn.Close();

            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
