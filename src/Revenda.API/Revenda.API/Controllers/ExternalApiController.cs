using Microsoft.AspNetCore.Mvc;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;

namespace Revenda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalApiController : ControllerBase
    {
        private readonly ILogger<ExternalApiController> _logger;
        private readonly IPedidoRepository _repo;
        public ExternalApiController(ILogger<ExternalApiController> logger, IPedidoRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// Efetuado um pedido de abstecimento para uma distribuidora.
        /// </summary>
        /// <returns>Dados do abastecimento</returns>
        /// <response code="200">Retorno da solicitação</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("supply")]
        [ProducesResponseType(typeof(RevendaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Supply([FromBody] AbastecimentoRequest request)
        {
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }     
    }
}
