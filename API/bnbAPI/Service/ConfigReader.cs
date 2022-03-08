using System.Collections.Generic;
using System.Text.Json;

namespace bnbAPI.Service
{
    public class ConfigReader
    {
        private readonly string[] expectedKeys = new string[]{
            "databaseName",
            "databasePort",
            "databaseHost",
            "databaseUser",
            "databasePassword",
            "clientSecret",
            "clientKey",
            "workFactor",
            "accessTokenValidity"
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

        
    }
}
