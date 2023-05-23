using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class RoomCategoryRepository : RepositoryGenericBase<RoomCategory>, IRoomCategoryRepository {
        public RoomCategoryRepository(MainDbContext dbContext, ILogger<RoomCategoryRepository> logger) : base(dbContext, logger)
        {
        }
    }
}