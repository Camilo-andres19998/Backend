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
        [Authorize]

        public dynamic eliminarTarea(Tareas tarea)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.validarToken(identity);

            if (!rToken.success) return rToken;

            Usuario usuario = rToken.result;


            if (usuario.rol != "administrador")
            {

                return new
                {
                    success = false,
                    message = "No tiene permisos de administrador para eliminar tarea",
                    result = tarea
                };
            }


            return new
            {
                success = true,
                message = "tarea eliminado",
                result = tarea
            };

        }


    

        [HttpGet]
        [Route("listarUsuarios")]
        public dynamic listarUsuarios()
        {

            List<Usuario> tareas = new List<Usuario>

            {

                new Usuario
                {
                   
                    nombre = "Camilo",
                     username = "Camilo0098",
                   // passrowdHash = "123.",
                    

                },

                new Usuario
                {
                    nombre = "Benjamin",
                    username = "Benjamin-98",
                    //passrowdHash = "1234.",
                     
                }

                };

            return tareas;
        }



    }


}