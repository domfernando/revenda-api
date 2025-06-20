using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revenda.API.Controllers;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Revenda.API.Tests
{
    public class ClienteControllerTests
    {
        private readonly Mock<ILogger<ClienteController>> _loggerMock;
        private readonly Mock<IClienteRepository> _repoMock;
        private readonly ClienteController _controller;

        public ClienteControllerTests()
        {
            _loggerMock = new Mock<ILogger<ClienteController>>();
            _repoMock = new Mock<IClienteRepository>();
            _controller = new ClienteController(_loggerMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenClienteExists()
        {
            // Arrange
            var clienteId = 1;
            var clienteResponse = new ClienteResponse { Id = clienteId };
            _repoMock.Setup(r => r.GetByID(clienteId)).ReturnsAsync(clienteResponse);

            // Act
            var result = await _controller.Get(clienteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(clienteResponse, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenClienteDoesNotExist()
        {
            // Arrange
            var clienteId = 1;
            _repoMock.Setup(r => r.GetByID(clienteId)).ReturnsAsync((ClienteResponse)null);

            // Act
            var result = await _controller.Get(clienteId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithListOfClientes()
        {
            // Arrange
            var clientes = new List<ClienteResponse>
            {
                new ClienteResponse { Id = 1 },
                new ClienteResponse { Id = 2 }
            };
            var condition = new GetClienteRequest(); // Create a valid condition object
            _repoMock.Setup(r => r.GetAll(condition)).ReturnsAsync(clientes);

            // Act
            var result = await _controller.GetAll(condition);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(clientes, okResult.Value);
        }
    }
}