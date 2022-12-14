using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
    [Route("autenticacion")]
    public class AutenticacionController : ControllerBase
    {
        public static Usuario usuario = new Usuario();
        private readonly IConfiguration _configuration;



        public AutenticacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [HttpPost("RegistroUsuario")]
        public async Task<ActionResult<Usuario>> Registro(UserDTO request)
        {
            CrearContraseniaHash(request.Passwd, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.nombre = request.Nombre;

            usuario.username = request.Usuario;
            usuario.PassrowdHash = passwordHash;
            usuario.PassrowdSalt = passwordSalt;

            return Ok(usuario);
        }


       
        private void CrearContraseniaHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }



        private bool VerificarContraseniaHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }




        }


        [HttpPost("IniciarSesion")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            if (usuario.username != request.Usuario)
            {
                return BadRequest("Usuario no existe.");
            }

            if (!VerificarContraseniaHash(request.Passwd, usuario.PassrowdHash, usuario.PassrowdSalt))
            {
                return BadRequest("Contraseña Incorrecta.");
            }

            string token = CrearToken(usuario);

             var refreshToken =  GenerarToken();
             EstablecerToken(refreshToken);

            return Ok(token);
        }



        private string CrearToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        private void EstablecerToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            usuario.RefreshToken = newRefreshToken.Token;
            usuario.TokenCreated = newRefreshToken.Created;
            usuario.TokenExpires = newRefreshToken.Expires;
        }



        private RefreshToken GenerarToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
                
            };

            return refreshToken;
        }


        }

    }


    
