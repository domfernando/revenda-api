using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Revenda.API.Controllers;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Revenda.API.Tests
{
    public class RevendaControllerTests
    {
        private readonly Mock<ILogger<RevendaController>> _loggerMock;
        private readonly Mock<IRevendaRepository> _repoMock;
        private readonly RevendaController _controller;

        public RevendaControllerTests()
        {
            _loggerMock = new Mock<ILogger<RevendaController>>();
            _repoMock = new Mock<IRevendaRepository>();
            _controller = new RevendaController(_loggerMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOk_WhenRevendaExists()
        {
            // Arrange
            var revendaId = 1;
            var revendaResponse = new RevendaResponse { Id = revendaId };
            _repoMock.Setup(r => r.GetByIdAsync(revendaId)).ReturnsAsync(revendaResponse);

            // Act
            var result = await _controller.Get(revendaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(revendaResponse, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenRevendaDoesNotExist()
        {
            // Arrange
            var revendaId = 1;
            _repoMock.Setup(r => r.GetByIdAsync(revendaId)).ReturnsAsync((RevendaResponse)null);

            // Act
            var result = await _controller.Get(revendaId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Add_ReturnsOk_WhenRevendaCreated()
        {
            // Arrange
            var request = new CreateRevendaRequest { RazaoSocial = "Teste", NomeFantasia = "Fantasia", CNPJ = "123", Email = "teste@email.com" };
            var response = new RevendaResponse { Id = 1, RazaoSocial = "Teste" };
            _repoMock.Setup(r => r.AddAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Add(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Add_ReturnsNotFound_WhenRevendaNotCreated()
        {
            // Arrange
            var request = new CreateRevendaRequest { RazaoSocial = "Teste" };
            _repoMock.Setup(r => r.AddAsync(request)).ReturnsAsync((RevendaResponse)null);

            // Act
            var result = await _controller.Add(request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsOk_WhenRevendaUpdated()
        {
            // Arrange
            var request = new UpdateRevendaRequest { Id = 1, RazaoSocial = "Atualizada" };
            var response = new RevendaResponse { Id = 1, RazaoSocial = "Atualizada" };
            _repoMock.Setup(r => r.UpdateAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenRevendaNotUpdated()
        {
            // Arrange
            var request = new UpdateRevendaRequest { Id = 1, RazaoSocial = "Atualizada" };
            _repoMock.Setup(r => r.UpdateAsync(request)).ReturnsAsync((RevendaResponse)null);

            // Act
            var result = await _controller.Update(request);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}