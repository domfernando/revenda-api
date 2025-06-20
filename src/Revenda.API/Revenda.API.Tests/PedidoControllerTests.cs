using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revenda.API.Controllers;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using Revenda.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Revenda.API.Tests
{
    public class PedidoControllerTests
    {
        private readonly Mock<ILogger<PedidoController>> _loggerMock;
        private readonly Mock<IPedidoRepository> _repoMock;
        private readonly Mock<IAbastecimentoService> _abastecimentoServiceMock;
        private readonly PedidoController _controller;

        public PedidoControllerTests()
        {
            _loggerMock = new Mock<ILogger<PedidoController>>();
            _repoMock = new Mock<IPedidoRepository>();
            _abastecimentoServiceMock = new Mock<IAbastecimentoService>();
            _controller = new PedidoController(_loggerMock.Object, _repoMock.Object, _abastecimentoServiceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenPedidoExists()
        {
            // Arrange
            var pedidoId = 1;
            var pedidoResponse = new PedidoResponse { Id = pedidoId };
            _repoMock.Setup(r => r.GetById(pedidoId)).ReturnsAsync(pedidoResponse);

            // Act
            var result = await _controller.Get(pedidoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(pedidoResponse, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenPedidoDoesNotExist()
        {
            // Arrange
            var pedidoId = 1;
            _repoMock.Setup(r => r.GetById(pedidoId)).ReturnsAsync((PedidoResponse)null);

            // Act
            var result = await _controller.Get(pedidoId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}