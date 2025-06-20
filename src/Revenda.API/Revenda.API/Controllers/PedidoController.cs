using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Application.Repositories;
using Revenda.Application.Services;
using System.ComponentModel;
using System.Net;

namespace Revenda.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoRepository _repo;
        private readonly IAbastecimentoService _abastecimentoService;
        public PedidoController(ILogger<PedidoController> logger, IPedidoRepository repo, IAbastecimentoService abastecimentoService)
        {
            _logger = logger;
            _repo = repo;
            _abastecimentoService = abastecimentoService;
        }

        /// <summary>
        /// Pesquisa um pedido de fornecimentoou abastecimento.
        /// </summary>
        /// <returns>Dados do pedido</returns>
        /// <response code="200">Detalhes do Pedido</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var obj = await _repo.GetById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Cria um pedido de fornecimento para um cliente.
        /// </summary>
        /// <returns>Dados do pedido</returns>
        /// <response code="200">Pedido criado com sucesso</response>
        /// <response code="404">Não oi possível criar o pedido</response>
        [HttpPost("fornece")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Fornece([FromBody] CreatePedidoRequest request)
        {
            var obj = await _repo.AddAsync(request);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Cria um pedido de abastecimento para uma revenda.
        /// </summary>
        /// <returns>Dados do pedido</returns>
        /// <response code="200">Pedido criado com sucesso</response>
        /// <response code="404">Não oi possível criar o pedido</response>
        [HttpPost("abastece")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Abastece([FromBody] CreatePedidoRequest request)
        {
            var obj = await _repo.AddAsync(request);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Atualiza um pedido de abastecimento ou fornecimento.
        /// </summary>
        /// <returns>Dados do pedido</returns>
        /// <response code="200">Pedido atualizado com sucesso</response>
        /// <response code="404">Não foi possível atualizar o pedido</response>
        [HttpPut]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdatePedidoRequest request)
        {
            var obj = await _repo.UpdateAsync(request);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        /// <summary>
        /// Solicita  um abastecimento à distribuidora.
        /// </summary>
        /// <returns>Dados do Abastecimento</returns>
        /// <response code="200">Abastecimento realizado com sucesso</response>
        /// <response code="404">Não foi possível solicitar o abastecimento</response>
        [HttpPost("solicita-abastecimento")]
        [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Abastecimento([FromBody] CreateAbastecimentoRequest request)
        {
            return await ResolveAbastecimento(request.PedidoId);
        }

        /// <summary>
        /// Processa lista de abastecimentos em fila (status 1).
        /// </summary>
        /// <returns>Dados dos Abastecimento processados</returns>
        /// <response code="200">Fila processada com sucesso</response>
        /// <response code="404">Não foi possível processar a fila</response>
        [HttpPost("processa-abastecimentos")]
        [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProcessaAbastecimentos([FromBody] ProcessaAbastecimentosRequest request)
        {
            var fila = await _repo.GetPedidosAbastecimento(request);
            var resultados = new List<object>();

            foreach (var item in fila)
            {
                var abastecimentoRequest = new CreateAbastecimentoRequest
                {
                    PedidoId = item.Id,
                    Itens = item.Itens
                };

                var result = await ResolveAbastecimento(abastecimentoRequest.PedidoId);

                if (result is OkObjectResult okResult)
                {
                    resultados.Add(new
                    {
                        PedidoId = item.Id,
                        Sucesso = true,
                        Dados = okResult.Value
                    });
                }
                else if (result is BadRequestObjectResult badRequestResult)
                {
                    resultados.Add(new
                    {
                        PedidoId = item.Id,
                        Sucesso = false,
                        Erro = badRequestResult.Value
                    });
                }
                else
                {
                    resultados.Add(new
                    {
                        PedidoId = item.Id,
                        Sucesso = false,
                        Erro = "Erro desconhecido"
                    });
                }
            }

            return Ok(resultados);
        }

        private async Task<IActionResult> ResolveAbastecimento(int pedidoId)
        {
            var requestAbastecimento = await _repo.GetToAbastecimento(pedidoId);

            var response = await _abastecimentoService.Supply(requestAbastecimento);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
