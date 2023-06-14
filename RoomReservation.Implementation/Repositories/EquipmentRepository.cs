using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class EquipmentRepository : RepositoryGenericBase<Equipment>, IEquipmentRepository {
        public EquipmentRepository(MainDbContext dbContext, ILogger<EquipmentRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<EquipmentDto>> BrowseAsync()
        {
            return await DbContext.Equipment.Where(x => x.IsDeleted == false).Select(x => new EquipmentDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }
    }
}