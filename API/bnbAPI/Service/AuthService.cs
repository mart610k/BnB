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

                    comm.CommandText = "INSERT INTO oauth2(username,accesstoken,refreshtoken,expires) VALUE (@username,UUID_TO_BIN(@accessToken),UUID_TO_BIN(@refreshToken),@expiry);";
                    comm.Parameters.AddWithValue("@username", userid);
                    comm.Parameters.AddWithValue("@accessToken", Guid.NewGuid().ToString());
                    comm.Parameters.AddWithValue("@refreshToken", Guid.NewGuid().ToString());

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

        public void UpdateAccessTokenForUser(string userID)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

                conn.Open();

                try
                {
                    MySqlCommand comm = conn.CreateCommand();

                    comm.CommandText = "UPDATE oauth2 SET accesstoken = UUID_TO_BIN(@accessToken),expires = @expiry WHERE username = @username;";
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

        /// <summary>
        /// Gets the user ID from the database, returns null when the user dont have an access token or the access token is no longer valid
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public string GetUserIDByAccessToken(string accesstoken)
        {
            string userID = null;


            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT username FROM oauth2 WHERE accesstoken = UUID_TO_BIN(@accesstoken) AND UTC_TIMESTAMP() < expires;";
            comm.Parameters.AddWithValue("@accesstoken", accesstoken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                userID = reader.GetString("username");
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

            comm.CommandText = "SELECT username FROM oauth2 WHERE refreshtoken = UUID_TO_BIN(@refreshToken)";
            comm.Parameters.AddWithValue("@refreshToken", refreshToken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                userID = reader.GetString("username");
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

            comm.CommandText = "SELECT BIN_TO_UUID(accesstoken),BIN_TO_UUID(refreshtoken),TIMESTAMPDIFF(SECOND,UTC_TIMESTAMP(), expires) as expiresin FROM oauth2 WHERE username = @userID";
            comm.Parameters.AddWithValue("@userID", userID);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                accessTokenDTO = new AccessTokenDTO();

                accessTokenDTO.Access_token =  reader.GetString("BIN_TO_UUID(accesstoken)");
                accessTokenDTO.Refresh_token= reader.GetString("BIN_TO_UUID(refreshtoken)");
                accessTokenDTO.Expires_in = reader.GetInt32("expiresin");
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

            comm.CommandText = "SELECT BIN_TO_UUID(accesstoken),BIN_TO_UUID(refreshtoken),TIMESTAMPDIFF(SECOND,UTC_TIMESTAMP(), expires) as expiresin FROM oauth2 WHERE accesstoken = UUID_TO_BIN(@accesstoken)";
            comm.Parameters.AddWithValue("@accesstoken", accessToken);

            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                accessTokenDTO = new AccessTokenDTO();

                accessTokenDTO.Access_token = reader.GetString("BIN_TO_UUID(accesstoken)");
                accessTokenDTO.Refresh_token = reader.GetString("BIN_TO_UUID(refreshtoken)");
                accessTokenDTO.Expires_in = reader.GetInt32("expiresin");
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

                comm.CommandText = "DELETE FROM oauth2 WHERE username = @UserName";
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
