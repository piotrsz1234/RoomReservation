using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.Aspects;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories
{
    [LogQueryTime]
    public class RoomCategoryRepository : RepositoryGenericBase<RoomCategory>, IRoomCategoryRepository
    {
        public RoomCategoryRepository(MainDbContext dbContext, ILogger<RoomCategoryRepository> logger) : base(dbContext, logger)
        {
        }
    }
}