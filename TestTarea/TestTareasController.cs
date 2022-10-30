

using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;


namespace TestTarea
{


    [TestClass]
    public class TestUsuarioController : ControllerBase



       // Tareas tareas = new Tareas();
        public IConfiguration _configuration;

        [Fact]
        public async Task ObtenerAcceso()
        {
            var mockUsersService = new Mock<ITareasServices>();
            mockUsersService
                .Setup(service => service.GetUsuarios())
                 .ReturnsAsync(new List<Usuario>()
                {
                    new()
                    {

                        nombre ="Camilo",
                        username="camilo98",
                        UserDTO = new UserDTO()
                        {
                            Passwd ="123",
                        }


                    }
                });



            var sut = new ObtenerUsuariosController(mockUsersService.Object);

            var result = (OkObjectResult)await sut.Get();


            result.StatusCode.Should().Be(200);

        }

    }