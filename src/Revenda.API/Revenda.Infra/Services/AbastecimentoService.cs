using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Revenda.Application.DTOs.Request;
using Revenda.Application.Repositories;
using Revenda.Application.Services;
using System.Net;
using System.Text;

namespace Revenda.Infra.Services
{
    public class AbastecimentoService : IAbastecimentoService
    {
        private readonly IConfigurationManager _config;
        private readonly HttpClient _httpClient;
        private readonly IPedidoRepository _pedidoRespository;
        private readonly IMapper _mapper;
        public string BaseUrl { get; set; }

        public AbastecimentoService(HttpClient httpClient, IConfigurationManager config, IPedidoRepository pedidoRespository, IMapper mapper)
        {
            _httpClient = httpClient;
            _config = config;
            BaseUrl = $"{_config.GetValue<string>("Distribuidora:Url")}";
            _pedidoRespository = pedidoRespository;
            _mapper = mapper;
        }

        public async Task<HttpResponseMessage> Supply(AbastecimentoRequest abastecimento)
        {
            var body = new
            {
                data = abastecimento
            };

            var json = System.Text.Json.JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}{_config.GetValue<string>("Distribuidora:SupplyEndpoint")}", content);

            var pedidoOriginal = await _pedidoRespository.GetById(abastecimento.PedidoId);
            pedidoOriginal.Tentativas = pedidoOriginal.Tentativas++;

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
            {
                pedidoOriginal.Status = 2;
                pedidoOriginal.Sucesso = true;
            }
            else
            {
                pedidoOriginal.Status = 1;
                pedidoOriginal.Sucesso = false;
            }

            var pedidoAtualizado = _mapper.Map<UpdatePedidoRequest>(pedidoOriginal);

            await _pedidoRespository.UpdateAsync(pedidoAtualizado);

            return response;
        }
    }
}
