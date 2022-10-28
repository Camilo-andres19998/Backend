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




        [HttpGet]
        [Route("listarId")]
        public dynamic listarClienteId(int codigo)
        {
            return new Cliente
            {
                id = codigo.ToString(),
                nombre = "Camilo Poblete",
                descripcion = "Cliente con tarea  no resuelta"
            };

        }




        [HttpPost]
        [Route("guardar")]

        public dynamic guardarCliente(Cliente cliente)
        {
            cliente.id = "3";

            return new
            {
                sucess = true,
                message = "cliente registrado",
                result = cliente

            };
        }



        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarCliente(Cliente cliente)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
          

            if (token != "camilo.")
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
                message = "cliente eliminado",
                result = cliente
            };
        }
    }
}
