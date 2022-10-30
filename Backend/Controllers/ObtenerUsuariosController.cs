using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObtenerUsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usersService;




        public ObtenerUsuariosController(IUsuariosService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet(Name = "ObtenerUsuarios")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetUsuarios();

            if (users.Any())

            {
                return Ok(users);
            }


            return NotFound();
        }

    }


}
    



