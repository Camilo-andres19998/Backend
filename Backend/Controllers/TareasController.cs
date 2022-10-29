using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{

    [ApiController]
    [Route("tarea")]
    public class TareasController : ControllerBase
    {
        public IConfiguration _configuration;


        [HttpGet]
        [Route("listar")]
        public dynamic listarTareas()
        {
           
            List<Tareas> tareas = new List<Tareas>

            {
              
                new Tareas
                {
                    id="1",
                    Username ="Camilo-98",
                    Nombre = "Camilo",
                    nombre_tarea = "Matematica.",
                    descripcion = "Usuario con tarea resuelta",
                    estado = "Resuelta",
                    fecha_creacion =   DateTime.Today,
                    fecha_actualizacion = DateTime.Now,
                   
                },


                  new Tareas
                {
                    id="2",
                    Username ="Daniel-98",
                    Nombre = "Camilo",
                    nombre_tarea = "Matematica.",
                    descripcion = "Usuario con tarea resuelta",
                    estado = "Resuelta",
                    fecha_creacion =   DateTime.Today,
                    fecha_actualizacion = DateTime.Now,

                },




                new Tareas
                {

                    id="3",
                    Username ="Benjamin-98",
                    Nombre = "Benjamin",
                    nombre_tarea = "Historia",
                    descripcion = "Usuario con tarea no resuelta",
                    estado = "No resuelta",
                    fecha_creacion =   DateTime.Today,
                    fecha_actualizacion = DateTime.Now,

                }

                };

            return tareas;
        }




        [HttpGet]
        [Route("listarId")]
        public dynamic listarTareasId(int codigo)
        {
            return new Tareas
            {
                id = codigo.ToString(),
                nombre_tarea = "Ciencias",
                descripcion = "Usuario con tarea  no resuelta",
                estado = "No resuelta"
                
            };

        }




        [HttpPost]
        [Route("guardar")]

        public dynamic guardarTareas(Tareas tarea)
        {
            tarea.id = "3";

            return new
            {
                sucess = true,
                message = "Tarea  registrada correctamente",
                result = tarea

            };
        }



        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarTarea(Tareas tarea)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
          

            if (token != "Matematica.")
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



      
        [HttpPost]
        [Route("login")]
        public dynamic Iniciarsesion([FromBody] Object optdata)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optdata.ToString());

            string user = data.username.ToString();
            string passwod = data.password.ToString();


            Usuario usuario = Usuario.DB().Where(x => x.username == user && x.Password== passwod).FirstOrDefault();

            if (usuario == null)
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
    }
}
