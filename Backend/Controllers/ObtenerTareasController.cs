using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObtenerTareasController : ControllerBase
    {
        private readonly ITareasServices _tareasService;




        public ObtenerTareasController(ITareasServices tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet(Name = "ObtenerTareas")]
        public async Task<IActionResult> Get()
        {
            var task = await _tareasService.GetTareas();

            if (task.Any())

            {
                return Ok(task);
            }


            return NotFound();
        }

    }


}
    



