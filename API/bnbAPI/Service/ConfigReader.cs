using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace bnbAPI.Service
{
    public class ConfigReader
    {
        private string[] expectedKeys = new string[]{
            "databaseName",
            "databasePort",
            "databaseHost",
            "databaseUser",
            "databasePassword",
            "clientSecret",
            "clientKey"
            };

        public Dictionary<string,string> ParseConfigFile(string jsonData)
        {
            JsonDocument json = JsonDocument.Parse(jsonData);

            return ReadValues(json.RootElement, expectedKeys);
        }

        private Dictionary<string,string> ReadValues(JsonElement jsonElement, string[] keys)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            for (int i = 0; i < keys.Length; i++)
            {
                try
                {
                    JsonElement datbaseNameElement = jsonElement.GetProperty(keys[i]);

                    keyValuePairs.Add(keys[i], datbaseNameElement.GetString());
                }
                catch(KeyNotFoundException)
                {
                    keyValuePairs.Add(keys[i], "");
                }
            }

            return keyValuePairs;
        }

        public Dictionary<string, string> ReadConfigFromFile(string filePath)
        {
            using (System.IO.StreamReader str = new System.IO.StreamReader(filePath))
            {
                return ParseConfigFile(str.ReadToEnd());
            }
        }
    }
}
