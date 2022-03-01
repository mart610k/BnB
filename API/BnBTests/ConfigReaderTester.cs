using bnbAPI.Service;
using NUnit.Framework;
using System.Collections.Generic;

namespace BnBTests
{
    class ConfigReaderTester
    {
        /// <summary>
        /// Reads the DatabaseName from JSON
        /// </summary>
        [TestCase("RandomName")]
        [TestCase("localhost")]
        [TestCase("231.242.21.1")]
        public void ReadJsonFileReturnsDatabaseName(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string,string> actual = configReader.ParseConfigFile("{  \"databaseName\" : \""+ expected +"\"}");
            
            Assert.AreEqual(expected, actual["databaseName"]);
        }

        /// <summary>
        /// Reads the DatabaseName from JSON
        /// </summary>
        [TestCase("RandomName")]
        [TestCase("localhost")]
        [TestCase("231.242.21.1")]
        [TestCase("https://localhost")]

        public void ReadJsonFileReturnsDatabaseHost(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"databaseHost\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["databaseHost"]);
        }

        /// <summary>
        /// Reads the DatabasePort from JSON
        /// </summary>
        [TestCase("3021")]
        [TestCase("4221")]
        [TestCase("321")]
        [TestCase("30")]
        public void ReadJsonFileReturnsDatabasePort(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"databasePort\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["databasePort"]);
        }

        /// <summary>
        /// Reads the DatabaseUser from JSON
        /// </summary>
        [TestCase("username")]
        [TestCase("bnbUser")]
        [TestCase("BNB")]
        [TestCase("fas321es")]
        public void ReadJsonFileReturnsUserName(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"databaseUser\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["databaseUser"]);
        }

        /// <summary>
        /// Reads the DatabasePassword from JSON
        /// </summary>
        [TestCase("passewat")]
        [TestCase("fas2dsargsdd4")]
        [TestCase("buih309s00saj")]
        [TestCase("guwe02erdshu7a94ejk")]
        public void ReadJsonFileReturnsDatabasePassword(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"databasePassword\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["databasePassword"]);
        }

        /// <summary>
        /// Reads the ClientKey from JSON
        /// </summary>
        [TestCase("daseosehma2421")]
        [TestCase("dsaiwoqw3832")]
        [TestCase("fsauh9fas0weyas")]
        [TestCase("faushr9rwq83ywhjsa")]
        public void ReadJsonFileReturnsClientId(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"clientKey\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["clientKey"]);
        }

        /// <summary>
        /// Reads the ClientSecret from JSON
        /// </summary>
        [TestCase("dasewqr1rfdsr2")]
        [TestCase("421rrfsa1dsa")]
        [TestCase("321rfasegsdt2")]
        [TestCase("he5yh65ijft43")]
        public void ReadJsonFileReturnsClientKey(string expected)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string, string> actual = configReader.ParseConfigFile("{  \"clientSecret\" : \"" + expected + "\"}");

            Assert.AreEqual(expected, actual["clientSecret"]);
        }


        /// <summary>
        /// Reads the full configuration file for as JSON
        /// </summary>
        [TestCase("BNB", "TestHost", "2124","username", "pastwwasdr", "cahdsaioi", "sdoapiueaposm")]
        public void ReadJsonFileReturnsFullConfiguration(string expectedDBname, string expectedDBHost, string expectedDBPort, string expectedDBUser,string expectedDBPassword, string expectedClientKey, string expectedClientSecret)
        {
            ConfigReader configReader = new ConfigReader();

            Dictionary<string,string> actualConfig = configReader.ParseConfigFile(
                "{" +
                "\"databaseHost\" : \"" + expectedDBHost + "\"," +
                "\"databasePort\" :  \"" + expectedDBPort + "\"," +
                "\"databaseName\" : \"" + expectedDBname + "\"," +
                "\"databaseUser\" : \"" + expectedDBUser + "\"," +
                "\"databasePassword\" : \"" + expectedDBPassword + "\"," +
                "\"clientKey\" : \"" + expectedClientKey + "\"," +
                "\"clientSecret\" : \"" + expectedClientSecret + "\" " +
                "}"
                );

            Assert.AreEqual(expectedDBname, actualConfig["databaseName"]);
            Assert.AreEqual(expectedDBPort, actualConfig["databasePort"]);
            Assert.AreEqual(expectedDBHost, actualConfig["databaseHost"]);
            Assert.AreEqual(expectedDBUser, actualConfig["databaseUser"]);
            Assert.AreEqual(expectedDBPassword, actualConfig["databasePassword"]);
            Assert.AreEqual(expectedClientKey, actualConfig["clientKey"]);
            Assert.AreEqual(expectedClientSecret, actualConfig["clientSecret"]);

        }
    }
}
