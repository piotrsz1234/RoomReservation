using System.Linq.Expressions;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IRepositoryGenericBase<T> where T : class, IEntity
    {
        Task<T?> GetOneAsync(int key, params Expression<Func<T, object>>[] includes);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task RemoveAsync(T entity);
        Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    }
}