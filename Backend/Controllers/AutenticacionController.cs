using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

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




        [HttpPost("Registro")]
        public async Task<ActionResult<Usuario>> Registro(UserDTO request)
        {
            CrearPasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            usuario.nombre = request.Nombre;
            usuario.username = request.Username;
            usuario.passrowdHash = passwordHash;
            usuario.passrowdSalt = passwordSalt;

            return Ok(usuario);
        }


        [HttpPost("Iniciar")]
        public async Task<ActionResult<string>> Login(UserDTO request)
        {
            if (usuario.username != request.Username)
            {
                return BadRequest("Usuario no existe.");
            }

            if (!VerificaContraseniaHash(request.Password, usuario.passrowdHash, usuario.passrowdSalt))
            {
                return BadRequest("Contraseña incorrecta.");
            }

            string token = CrearToken(usuario);

            var refreshToken = GenerarRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }


        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificaContraseniaHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CrearToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.username),
                new Claim(ClaimTypes.Role, "Admin")
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





        private RefreshToken GenerarRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }



        private void SetRefreshToken(RefreshToken newRefreshToken)
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

    }





}

