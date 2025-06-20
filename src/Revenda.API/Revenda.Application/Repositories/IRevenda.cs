using Revenda.Application.DTOs.Request;
using Revenda.Application.DTOs.Response;

namespace Revenda.Application.Repositories
{
    public interface IRevendaRepository
    {
        Task<RevendaResponse> GetByIdAsync(int id);
        Task<List<RevendaResponse>> GetAllAsync(GetRevendaRequest condition);
        Task<RevendaResponse> AddAsync(CreateRevendaRequest request);
        Task<RevendaResponse> UpdateAsync(UpdateRevendaRequest request);
    }
}
