using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObtenerController : ControllerBase
    {
        private readonly IUsersService _usersService;




        public ObtenerController(IUsersService usersService)
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
    



