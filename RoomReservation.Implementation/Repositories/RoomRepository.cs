using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.Aspects;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories
{
    [LogQueryTime]
    public class RoomRepository : RepositoryGenericBase<Room>, IRoomRepository
    {
        public RoomRepository(MainDbContext dbContext, ILogger<RoomRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<RoomDto>> BrowseAsync(int buildingId)
        {
            try
            {
                return await DbContext.Room.Where(x => x.IsDeleted == false && x.Building.IsDeleted == false)
                    .Include(x => x.RoomCategories).Include("RoomCategories.Category").Include(x => x.EquipmentRooms).Include("EquipmentRooms.Equipment")
                    .Include(x => x.Building)
                    .Where(x => x.BuildingId == buildingId)
                    .Select(x => new RoomDto
                    {
                        Id = x.Id,
                        RoomNumber = x.RoomNumber,
                        MaxPeople = x.MaxPeople,
                        BuildingId = x.BuildingId,
                        Equipment = x.EquipmentRooms.Where(y => y.IsDeleted == false).Select(y => new EquipmentDto
                        {
                            Id = y.Equipment.Id,
                            Name = y.Equipment.Name
                        }).ToList(),
                        Categories = x.RoomCategories.Where(y => y.IsDeleted == false).Select(y => new CategoryDto
                        {
                            Id = y.Category.Id,
                            Name = y.Category.Name
                        }).ToList()
                    }).ToListAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<RoomDto?> GetOneDto(int id)
        {
            try
            {
                return await DbContext.Room.Where(x => x.IsDeleted == false && x.Building.IsDeleted == false)
                    .Include(x => x.RoomCategories).Include("RoomCategories.Category").Include(x => x.EquipmentRooms).Include("EquipmentRooms.Equipment")
                    .Include(x => x.Building)
                    .Where(x => x.Id == id)
                    .Select(x => new RoomDto
                    {
                        Id = x.Id,
                        RoomNumber = x.RoomNumber,
                        MaxPeople = x.MaxPeople,
                        BuildingId = x.BuildingId,
                        Equipment = x.EquipmentRooms.Select(y => new EquipmentDto
                        {
                            Id = y.Equipment.Id,
                            Name = y.Equipment.Name
                        }).ToList(),
                        Categories = x.RoomCategories.Select(y => new CategoryDto
                        {
                            Id = y.Category.Id,
                            Name = y.Category.Name
                        }).ToList()
                    }).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}