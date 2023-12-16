using System.Collections;

namespace FrontEndWebApplication.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable> GetAllAsync(string url);
        Task<T> GetByIdAsync(string url, int id);
        Task<bool> PostAsync(string url, T entity);
        Task<bool> UpdateAsync(string url, T entity);
        Task<bool> DeleteAsync(string url, int id);

    }
}