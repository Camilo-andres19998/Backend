using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Backend.Controllers
{

    [Route("autenticacion")]
    public class Autenticacion :ControllerBase
    {
        public static Usuario usuario = new Usuario();


        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Registro(UserDTO request)
        {
            CrearPasswordHash(request.Passwd, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.username = request.Usuario;
            usuario.passrowdHash = passwordHash;
            usuario.passrowdSalt = passwordSalt;

            return Ok(usuario);
        }

        private void CrearPasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }



    }
}
