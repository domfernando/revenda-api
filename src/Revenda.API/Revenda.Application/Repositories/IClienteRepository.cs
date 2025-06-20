using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;

namespace Revenda.Application.Repositories
{
    public interface IClienteRepository
    {
        Task<ClienteResponse> GetByID(int id);
        Task<List<ClienteResponse>> GetAll(GetClienteRequest condition);
        Task<ClienteResponse> AddAsync(CreateClienteRequest request);
        Task<ClienteResponse> UpdateAsync(UpdateClienteRequest request);
    }
}
