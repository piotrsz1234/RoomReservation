using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class BuildingRepository : RepositoryGenericBase<Building>, IBuildingRepository {
        public BuildingRepository(MainDbContext dbContext, ILogger<BuildingRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<BuildingDto>> BrowseAsync()
        {
            try
            {
                var query = DbContext.Building.Where(x => x.IsDeleted == false).Select(x => new BuildingDto()
                {
                    Id=x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    PostalCode = x.PostalCode,
                });

                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}