using Backend.Controllers;
using Backend.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using System.Threading.Tasks;
using Xunit;
using Moq;
using Backend.Services;

namespace TestUnitario
{


    [TestClass]
    public class TestTareasController : ControllerBase

    {
        Tareas tareas = new Tareas();
        public IConfiguration _configuration;

        [Fact]
        public async Task ObtenerTareas()
        {
            var mockUsersService = new Mock<ITareasServices>();
            mockUsersService
                .Setup(service => service.GetTareas())
                 .ReturnsAsync(new List<Tareas>()
                {
                    new()
                    {

                        nombre_tarea ="Ciencias",
                        estado="Resuelta",
                        descripcion="Tarea completa y resuelta",
                        fecha_creacion= DateTime.Today,
                        fecha_actualizacion=DateTime.Now,

                        usuario = new Usuario()
                        {
                            nombre ="Camilo",
                        }


                    }
                });



            var sut = new ObtenerTareasController(mockUsersService.Object);

            var result = (OkObjectResult)await sut.Get();


            result.StatusCode.Should().Be(200);

        }





        [Fact]
        public async Task ObtenerTareasServices()
        {
            var mockUsersService = new Mock<ITareasServices>();

            mockUsersService
                .Setup(service => service.GetTareas())
                .ReturnsAsync(new List<Tareas>());

            var sut = new ObtenerTareasController(mockUsersService.Object);

            var result = await sut.Get();


            mockUsersService.Verify(
                service => service.GetTareas(),
                Times.Once()

     );

        }

        [Fact]
        public async Task ObtenerListaTareas()
        {
            var mockUsersService = new Mock<ITareasServices>();

            mockUsersService
                .Setup(service => service.GetTareas())
                .ReturnsAsync(new List<Tareas>()
                {
                    new()
                    {
                        nombre_tarea ="Lenguaje",
                        estado="No resuelta",
                        descripcion="Falta poco en resolver",
                        fecha_creacion= DateTime.Today,
                        fecha_actualizacion=DateTime.Now,

                        usuario = new Usuario()
                        {
                            nombre ="Benjamin",
                        }



                      }
                });



            var sut = new ObtenerTareasController(mockUsersService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<Tareas>>();

        }


        [Fact]
        public async Task TareasNoEncontradas404()
        {
            var mockUsersService = new Mock<ITareasServices>();

            mockUsersService
                .Setup(service => service.GetTareas())
                .ReturnsAsync(new List<Tareas>());

            var sut = new ObtenerTareasController(mockUsersService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);


        }

    }



}