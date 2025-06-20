using Microsoft.AspNetCore.Mvc;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;

namespace Revenda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteRepository = clienteRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _clienteRepository.GetByID(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetClienteRequest condition)
        {
            var clientes = await _clienteRepository.GetAll(condition);
            return Ok(clientes);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClienteRequest request)
        {
            // Implementation for adding a new cliente
            return CreatedAtAction(nameof(Get), new { id = 1 }, null); // Example response
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClienteRequest request)
        {
            // Implementation for updating a cliente
            return NoContent(); // Example response
        }
    }
}
