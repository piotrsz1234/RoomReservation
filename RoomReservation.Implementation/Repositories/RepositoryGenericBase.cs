using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public abstract class RepositoryGenericBase<T> : IRepositoryGenericBase<T> where T : class, IEntity {
        protected readonly MainDbContext DbContext;
        protected readonly ILogger Logger;

        internal RepositoryGenericBase(MainDbContext dbContext, ILogger logger)
        {
            DbContext = dbContext;
            Logger = logger;
        }

        public async Task<T?> GetOneAsync(long key, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(x => x.Id == key);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.FirstOrDefaultAsync(predicate);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> expression,
            params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = DbContext.Set<T>().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await DbContext.Set<T>().AnyAsync(expression);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    await DbContext.Set<T>().AddAsync(entity);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task Remove(T entity)
        {
            try
            {
                DbContext.Set<T>().Remove(entity);

                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}