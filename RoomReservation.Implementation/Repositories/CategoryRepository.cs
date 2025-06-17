using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.Aspects;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories
{
    [LogQueryTime]
    public class CategoryRepository : RepositoryGenericBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MainDbContext dbContext, ILogger<CategoryRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<CategoryDto>> BrowseAsync()
        {
            try
            {
                return await DbContext.Category.Where(x => x.IsDeleted == false).Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}