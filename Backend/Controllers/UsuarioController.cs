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





        /*
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public dynamic Iniciarsesion([FromBody] Object optdata)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optdata.ToString());

            string user = data.username.ToString();
            string passwod = data.password.ToString();


           // UserDTO usuario = UserDTO.DB().Where(x => x.username == user && x. == passwod).FirstOrDefault();

            if ( usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }
           
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id",usuario.idUsuario),
                new Claim("username",usuario.username)




            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: singIn





         );

            return new
            {
                success = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };

        }

        */


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

                    //nombre = "Camilo",
                    //usu = "Camilo0098",
                    //passrowd = "123.",


                },

                new Usuario
                {
                    //nombre = "Benjamin",
                    //usuario = "Benjamin-98",
                   // passrowd = "1234.",

                }

                };

            return tareas;
        }



    }


}

