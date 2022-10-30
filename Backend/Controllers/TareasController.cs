using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{

    [ApiController]
    [Route("tarea")]
    public class TareasController : ControllerBase
    {


        [HttpGet]
        [Route("listarTareas")]
        public dynamic listarTareas()
        {

            List<Tareas> tareas = new List<Tareas>()
            {

                    new()
                    {

                        nombre_tarea ="Matematica",
                        estado ="Resuelta",
                        
                        usuario = new Usuario()
                        {
                            nombre ="Camilo",
                            username ="camilo-98"
                        }


                    },

                      new()
                      {

                        nombre_tarea ="Ciencias",
                        estado ="No Resuelta",
                        usuario = new Usuario()
                      {
                           nombre ="Benjamin",
                           username ="benjamin-98"
                        }


                      }
                };





            return tareas;
        }






        [HttpGet]
        [Route("listarId")]
        public dynamic listarTareasId(int codigo)
        {
            return new Tareas
            {
                id = codigo.ToString(),
                nombre_tarea = "Ciencias",
                descripcion = "Usuario con tarea  no resuelta"
            };

        }




        [HttpPost]
        [Route("guardar")]

        public dynamic guardarTareas(Tareas tarea)
        {
            tarea.id = "3";

            return new
            {
                sucess = true,
                message = "Tarea  registrada correctamente",
                result = tarea

            };
        }



        [HttpPost]
        [Route("eliminar")]
        public dynamic eliminarTarea(Tareas tarea)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
          

            if (token != "Matematica.")
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
                message = "tarea eliminada",
                result = tarea
            };
        }
    }
}
