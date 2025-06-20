namespace Revenda.Application.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<List<T>> GetAll(T condition);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
