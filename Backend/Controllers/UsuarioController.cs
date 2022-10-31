using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{
    [ApiController]
    [Route("usuario")]



    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;


        public UsuarioController(IConfiguration config)
        {
            _configuration = config;
        }
       
        private string GenerateJSONWebToken(Usuario userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarTarea(Tareas tarea)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;


            if (token != "Alejandro")
            {
                return new
                {
                    success = false,
                    message = "token incorrecto",
                    result = ""
                };
            }

            return new
            {
                success = true,
                message = "tarea eliminada",
                result = tarea
            };
        }
    




        [HttpGet]
        [Route("listarUsuarios")]
        public dynamic listarUsuarios()
        {

            List<Usuario> usuarios = new List<Usuario>()
            {

                    new()
                    {

                        nombre ="Camilo",
                        UserDTO = new UserDTO()
                        {
                            Usuario ="camilo-98",
                            Passwd ="123"
                        }


                    },

                      new()
                      {

                        nombre ="Benjamin",
                        UserDTO = new UserDTO()
                        {
                            Usuario ="benjamin-98",
                            Passwd ="123"
                        }


                      }
                };





            return usuarios;
        }



    }


}