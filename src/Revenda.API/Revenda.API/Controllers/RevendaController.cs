using Microsoft.AspNetCore.Mvc;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;

namespace Revenda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevendaController : ControllerBase
    {
        private readonly ILogger<RevendaController> _logger;
        private readonly IRevendaRepository _repo;
        public RevendaController(ILogger<RevendaController> logger, IRevendaRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        /// <summary>
        /// Pesquisa uma revenda .
        /// </summary>
        /// <returns>Dados da revenda</returns>
        /// <response code="200">Dados da Revenda</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RevendaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _repo.GetByIdAsync(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Criar uma nova revenda
        /// </summary>
        /// <returns>Dados da revenda</returns>
        /// <response code="200">Dados da Revenda</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost]
        [ProducesResponseType(typeof(RevendaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] CreateRevendaRequest request)
        {
            var obj = await _repo.AddAsync(request);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Atualiza os dados de uma revenda.
        /// </summary>
        /// <returns>Dados da revenda</returns>
        /// <response code="200">Detalhes da revenda</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut]
        [ProducesResponseType(typeof(RevendaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateRevendaRequest request)
        {
            var obj = await _repo.UpdateAsync(request);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }
    }
}
