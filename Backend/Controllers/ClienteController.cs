using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {


        [HttpGet]
        [Route("listar")]
        public dynamic listarCliente()
        {
            List<Cliente> clientes = new List<Cliente>

            {
                new Cliente
                {
                    id = "1",
                    nombre = "Camilo",
                    descripcion = "Cliente con tarea resuelta"
                },

                new Cliente
                {
                    id = "2",
                    nombre = "Benjamin",
                    descripcion = "Cliente con tarea resuelta"
                }

                };

            return clientes;
        }

    }

}

