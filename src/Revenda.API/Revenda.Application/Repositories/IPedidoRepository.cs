using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;
using Revenda.Domain.Entities;

namespace Revenda.Application.Repositories
{
    public interface IPedidoRepository
    {
        Task<PedidoResponse> GetById(int id);
        Task<List<PedidoResponse>> GetAll(GetPedidoRequest condition);
        Task<PedidoResponse> AddAsync(CreatePedidoRequest entity);
        Task<PedidoResponse> UpdateAsync(UpdatePedidoRequest entity);
        Task<AbastecimentoRequest> GetToAbastecimento(int pedidoId);
        Task<Pedido> UpdateToAbastecimento(Pedido rquest);
        Task<List<AbastecimentoResponse>> GetPedidosAbastecimento(ProcessaAbastecimentosRequest request);
    }
}
