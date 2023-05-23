using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class RoomRepository : RepositoryGenericBase<Room>, IRoomRepository{
        public RoomRepository(MainDbContext dbContext, ILogger<RoomRepository> logger) : base(dbContext, logger)
        {
        }
    }
}