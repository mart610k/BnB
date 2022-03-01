using bnbAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public static string GetConnectionString()
        {
            return string.Format("Server={0},Port={1};Database={2};Uid={3};Pwd={4};",DBHost,DBPort,DBName,DBUser,DBPassword);
        }

        public static void ReadConfiguration()
        {
            ConfigReader reader = new ConfigReader();

            Dictionary<string,string> values = reader.ReadConfigFromFile(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Config.json"));

            DBHost = values["databaseHost"];
            DBPort = values["databasePort"];
            DBName = values["databaseName"];
            DBUser = values["databaseUser"];
            DBPassword = values["databasePassword"];
            ClientKey = values["clientKey"];
            ClientSecret = values["clientSecret"];






        }

    }
}
