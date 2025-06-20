using Revenda.Domain.Entities;

namespace Revenda.Application.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto?> GetById(int id);
        Task<List<Produto>> GetAll(Produto condition);
        Task<Produto> AddAsync(Produto entity);
        Task<Produto> UpdateAsync(Produto entity);
        Task DeleteAsync(int id);
    }
}
