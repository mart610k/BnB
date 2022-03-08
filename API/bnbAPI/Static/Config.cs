using bnbAPI.Service;
using System.Collections.Generic;

namespace bnbAPI.Static
{
    public class Config
    {
        public static string DBHost { get; private set; }
        public static string DBPort { get; private set; }
        public static string DBName { get; private set; }
        public static string DBUser { get; private set; }
        public static string DBPassword { get; private set; }
        public static string ClientKey { get; private set; }
        public static string ClientSecret { get; private set; }

        public static int WorkFactor { get; private set; }
        public static int AccessTokenValidity { get; private set; }



        public static string GetConnectionString()
        {
            return string.Format("Server={0},Port={1};Database={2};Uid={3};Pwd={4};", DBHost, DBPort, DBName, DBUser, DBPassword);
        }

        public static void ReadConfiguration()
        {

            using (System.IO.StreamReader str = new System.IO.StreamReader(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Config.json")))
            {
                ReadConfigurationFromJSON(str.ReadToEnd());
            }
        }
  
        
        public static void ReadConfigurationFromJSON(string Json)
        {
            ConfigReader reader = new ConfigReader();
            Dictionary<string, string> values = reader.ParseConfigFile(Json);


            DBHost = values["databaseHost"];
            DBPort = values["databasePort"];
            DBName = values["databaseName"];
            DBUser = values["databaseUser"];
            DBPassword = values["databasePassword"];
            ClientKey = values["clientKey"];
            ClientSecret = values["clientSecret"];
            WorkFactor = int.Parse(values["workFactor"]);
            AccessTokenValidity = int.Parse(values["accessTokenValidity"]);
        }
    }
}
