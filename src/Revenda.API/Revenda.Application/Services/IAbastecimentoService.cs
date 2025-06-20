using Revenda.Application.DTOs.Request;

namespace Revenda.Application.Services
{
    public interface IAbastecimentoService
    {
        public string BaseUrl { get; set; }
        public Task<HttpResponseMessage> Supply(AbastecimentoRequest abastecimento);
    }
}
