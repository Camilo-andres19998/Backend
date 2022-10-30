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

namespace TestUsuarios
{


    [TestClass]
    public class TestTareasontroller : ControllerBase

    {
        Tarea usuario = new Usuario();
        public IConfiguration _configuration;

        [Fact]
        public async Task ObtenerAcceso()
        {
            var mockUsersService = new Mock<IUsersService>();
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





        [Fact]
        public async Task ObtenerInvokesUsuariosServices()
        {
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService
                .Setup(service => service.GetUsuarios())
                .ReturnsAsync(new List<Usuario>());

            var sut = new ObtenerUsuariosController(mockUsersService.Object);

            var result = await sut.Get();


            mockUsersService.Verify(
                service => service.GetUsuarios(),
                Times.Once()

     );

        }

        [Fact]
        public async Task ObtenerListaUsuarios()
        {
            var mockUsersService = new Mock<IUsersService>();

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

            var result = await sut.Get();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<Usuario>>();

        }


        [Fact]
        public async Task UsuarioNoEncontrados404()
        {
            var mockUsersService = new Mock<IUsersService>();

            mockUsersService
                .Setup(service => service.GetUsuarios())
                .ReturnsAsync(new List<Usuario>());

            var sut = new ObtenerUsuariosController(mockUsersService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);


        }

    }



    }
