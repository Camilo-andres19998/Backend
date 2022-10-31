using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Backend.Controllers
{

    [ApiController]
    [Route("tarea")]
    public class TodaslasTareasController : ControllerBase
    {

       

        private static List<Tareas> task = new List<Tareas>()
        {
         
              new Tareas()
                {

                    id = 1,
                    nombre_tarea = "Matematica",
                    estado = "Resuelta",
                    descripcion= "Completamente resuelta",
                    usuario = new Usuario
                    {
                        nombre="Camilo",
                        username="Camio098"
                    }


                 },

                new Tareas()
                {

                    id = 2,
                    nombre_tarea = "Lenguaje",
                    estado = "Resuelta",
                    descripcion= "Completamente resuelta",
                    usuario = new Usuario
                    {
                        nombre="Benjamin",
                        username="Benjamin098"


                    }
                    },


                    new Tareas()
                    {
                      id = 3,
                      nombre_tarea = "Historia",
                      estado = "Resuelta",
                      descripcion= "Completamente resuelta",
                      usuario = new Usuario
                     {
                        nombre="Daniel",
                        username="Daniel098"
                    },



                    }



                };





        [HttpGet]
        [Route("TodaslasTareas")]
        public async Task<ActionResult<Tareas>> GetTareas()
        {

            return Ok(task);
        }



        [HttpGet]
        [Route("listadoTareaAsociada")]
        public async Task<ActionResult<Tareas>> GetTarea(int id)
        {
            var tarea = task.Find(x => x.id == id);
            if (tarea == null)
                return BadRequest("No se encuentra la tarea");

            return Ok(tarea);
        }


        [HttpPost]
        [Route("AgregarTareaAsociada")]
        public async Task<ActionResult<Tareas>>AgregarTarea(Tareas request)
        {
            task.Add(request);
            return Ok(task);
        }


        [HttpDelete]
        [Route("EliminarTarea")]
        public async Task<ActionResult<Tareas>> EliminarTareas(int id)
        {
            var tareas = task.Find(x => x.id == id);
            if (tareas == null)
            {
                return BadRequest("La tarea del usuario no se encuentra!");
            }

            task.Remove(tareas);
            return Ok(task);
        }


    }


}

