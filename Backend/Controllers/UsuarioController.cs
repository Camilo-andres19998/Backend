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



        
      

        


        [HttpPost]
        [Route("eliminarTarea")]
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
                message = "tarea eliminada",
                result = tarea
            };

        }

        
     
     
       



    }

}
