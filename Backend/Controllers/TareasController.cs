using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers
{

    [ApiController]
    [Route("tarea")]
    public class TareasController : ControllerBase
    {
        private readonly ITareasServices _tareasService;

        public IConfiguration _configuration;


        [HttpGet]
        [Route("listarTareas")]
        public dynamic listarTareas()
        {

            List<Tareas> tareas = new List<Tareas>()
            {

                    new()
                    {
                        id= 1,
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
                        id = 2,
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
                id = 3,
                nombre_tarea = "Ciencias",
                descripcion = "Usuario con tarea  no resuelta"
            };

        }




        [HttpPost]
        [Route("guardar")]

        public dynamic guardarTareas(Tareas tarea)
        {
            tarea.id = 3;

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



    







