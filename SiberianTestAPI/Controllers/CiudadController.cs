using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SiberianTestAPI.Models;

namespace SiberianTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=SiberianDB;Uid=root;password='';";
        private MySqlConnection connection = new MySqlConnection(connectionString);

        [HttpGet]
        [Route("all")]
        public IActionResult MostrarTodo()
        {
            List<Ciudad> cities = new List<Ciudad>();

            connection.Open();
            string query = "SELECT IDCiudad,NombreCiudad FROM Ciudad";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Ciudad city = new Ciudad();
                city._nombre_ciudad = reader["NombreCiudad"].ToString();
                city._id_ciudad = int.Parse(reader["IDCiudad"].ToString());

                cities.Add(city);
            }

            connection.Close();


            return Ok(cities);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult Buscar(int id)
        {
            List<Ciudad> cities = new List<Ciudad>();

            connection.Open();
            string query = "SELECT IDCiudad,NombreCiudad FROM Ciudad WHERE IDCiudad = '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Ciudad city = new Ciudad();
                city._nombre_ciudad = reader["NombreCiudad"].ToString();
                city._id_ciudad = int.Parse(reader["IDCiudad"].ToString());

                cities.Add(city);
            }

            connection.Close();


            return Ok(cities);
        }

        [HttpPost]
        [Route("insert")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Insert(Ciudad city)
        {
            int result = 0;

            connection.Open();
            string query = "INSERT INTO Ciudad (NombreCiudad) VALUES(@NombreCiudad)";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NombreCiudad", city._nombre_ciudad);

            result = cmd.ExecuteNonQuery();

            connection.Close();


            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(Ciudad city)
        {
            int result = 0;

            connection.Open();
            string query = "UPDATE Ciudad SET NombreCiudad = @NombreCiudad WHERE IDCiudad = '" + city._id_ciudad + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NombreCiudad", city._nombre_ciudad);

            result = cmd.ExecuteNonQuery();

            connection.Close();

            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            int result = 0;

            connection.Open();
            string query = "DELETE FROM Ciudad WHERE IDCiudad = '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(query, connection);

            result = cmd.ExecuteNonQuery();

            connection.Close();


            return Ok(result);
        }
    }
}
