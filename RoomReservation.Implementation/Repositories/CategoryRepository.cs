using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class CategoryRepository : RepositoryGenericBase<Category>, ICategoryRepository {
        public CategoryRepository(MainDbContext dbContext, ILogger<CategoryRepository> logger) : base(dbContext, logger)
        {
        }
    }
}