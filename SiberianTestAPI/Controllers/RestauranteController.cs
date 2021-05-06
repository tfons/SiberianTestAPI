using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using SiberianTestAPI.Models;

namespace SiberianTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=SiberianDB;Uid=root;password='';";
        private MySqlConnection connection = new MySqlConnection(connectionString);
        private IConfiguration _config;

        /*
            PARAMETRO SP_RESTAURANES:
            _option: 
                null: mostrar todo,
                "S": buscar restaurante por ID,
                "I": insertar un restaurante,
                "U": actualizar un restaurante,
                "D": eliminar un restaurante
         */

        public RestauranteController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("auth")]
        public IActionResult Auth()
        {
            List<string> tk = new List<string>();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cl@ve$ecreta2021"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            tk.Add(tokenString);

            return Ok(tk);
        }


        [HttpGet]
        [Route("all")]
        public IActionResult MostrarTodo()
        {
            List<Restaurante> lista = new List<Restaurante>();

            connection.Open();
            string query = "Sp_Restauranes";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("?_option", null);
            cmd.Parameters.AddWithValue("?_nombre_restaurante", null);
            cmd.Parameters.AddWithValue("?_id_restaurante", null);
            cmd.Parameters.AddWithValue("?_id_ciudad", null);
            cmd.Parameters.AddWithValue("?_numero_aforo", null);
            cmd.Parameters.AddWithValue("?_telefono", null);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Restaurante res = new Restaurante();

                res._id_restaurante = int.Parse(reader["IDRestaurante"].ToString());
                res._nombre_restaurante = reader["NombreRestaurante"].ToString();
                res._id_ciudad = int.Parse(reader["IDCiudad"].ToString());
                res._nombre_ciudad = reader["NombreCiudad"].ToString();
                res._numero_aforo = int.Parse(reader["NumeroAforo"].ToString());
                res._telefono = reader["Telefono"].ToString();
                res._fecha_creacion = reader["FechaCreacion"].ToString();

                lista.Add(res);
            }
            connection.Close();

            return Ok(lista);
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public IActionResult GetRestaurante(int id)
        {
            List<Restaurante> lista = new List<Restaurante>();

            connection.Open();
            string query = "Sp_Restauranes";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("?_option", "S");
            cmd.Parameters.AddWithValue("?_nombre_restaurante", null);
            cmd.Parameters.AddWithValue("?_id_restaurante", id);
            cmd.Parameters.AddWithValue("?_id_ciudad", null);
            cmd.Parameters.AddWithValue("?_numero_aforo", null);
            cmd.Parameters.AddWithValue("?_telefono", null);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Restaurante res = new Restaurante();

                res._id_restaurante = int.Parse(reader["IDRestaurante"].ToString());
                res._nombre_restaurante = reader["NombreRestaurante"].ToString();
                res._id_ciudad = int.Parse(reader["IDCiudad"].ToString());
                res._nombre_ciudad = reader["NombreCiudad"].ToString();
                res._numero_aforo = int.Parse(reader["NumeroAforo"].ToString());
                res._telefono = reader["Telefono"].ToString();
                res._fecha_creacion = reader["FechaCreacion"].ToString();

                lista.Add(res);
            }
            connection.Close();

            return Ok(lista);
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(Restaurante res)
        {
            int result = 0;

            connection.Open();
            string query = "Sp_Restauranes";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("?_option", "I");
            cmd.Parameters.AddWithValue("?_nombre_restaurante", res._nombre_restaurante);
            cmd.Parameters.AddWithValue("?_id_restaurante", null);
            cmd.Parameters.AddWithValue("?_id_ciudad", res._id_ciudad);
            cmd.Parameters.AddWithValue("?_numero_aforo", res._numero_aforo);
            cmd.Parameters.AddWithValue("?_telefono", res._telefono);

            result = cmd.ExecuteNonQuery();

            connection.Close();


            return Ok(result);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(Restaurante res)
        {
            int result = 0;

            connection.Open();
            string query = "Sp_Restauranes";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("?_option", "U");
            cmd.Parameters.AddWithValue("?_nombre_restaurante", res._nombre_restaurante);
            cmd.Parameters.AddWithValue("?_id_restaurante", res._id_restaurante);
            cmd.Parameters.AddWithValue("?_id_ciudad", res._id_ciudad);
            cmd.Parameters.AddWithValue("?_numero_aforo", res._numero_aforo);
            cmd.Parameters.AddWithValue("?_telefono", res._telefono);

            result = cmd.ExecuteNonQuery();

            connection.Close();


            return Ok(result);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int result = 0;

            connection.Open();
            string query = "Sp_Restauranes";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("?_option", "D");
            cmd.Parameters.AddWithValue("?_nombre_restaurante", null);
            cmd.Parameters.AddWithValue("?_id_restaurante", id);
            cmd.Parameters.AddWithValue("?_id_ciudad", null);
            cmd.Parameters.AddWithValue("?_numero_aforo", null);
            cmd.Parameters.AddWithValue("?_telefono", null);

            result = cmd.ExecuteNonQuery();

            connection.Close();


            return Ok(result);
        }
    }
}
