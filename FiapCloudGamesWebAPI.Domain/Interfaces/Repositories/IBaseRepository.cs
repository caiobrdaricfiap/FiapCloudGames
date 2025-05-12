using FiapCloudGames.Domain.Entities;
using System.Linq.Expressions;

namespace FiapCloudGameWebAPI.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
