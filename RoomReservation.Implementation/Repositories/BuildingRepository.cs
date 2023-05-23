using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class BuildingRepository : RepositoryGenericBase<Building>, IBuildingRepository {
    public BuildingRepository(MainDbContext dbContext, ILogger<BuildingRepository> logger) : base(dbContext, logger)
    {
    }
    }
}