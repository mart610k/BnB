using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using bnbAPI.Static;
using MySql.Data.MySqlClient;

namespace bnbAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetMysqlVersion()
        {
            MySqlConnection conn = new MySqlConnection(Config.GetConnectionString());

            MySqlCommand comm = conn.CreateCommand();

            comm.CommandText = "SELECT VERSION();";
            
            conn.Open();

            MySqlDataReader reader = comm.ExecuteReader();

            string Version= "";
            while (reader.Read())
            {
                 Version = reader.GetString("VERSION()");
            }

            conn.Close();
            
            return Ok(Version);
        }
    }
}
