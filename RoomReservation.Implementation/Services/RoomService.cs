using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Contracts.Room.Models;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services {
    internal sealed class RoomService : ServiceBase, IRoomService {
        private readonly IRoomRepository _roomRepository;

        public RoomService(ILogger<RoomService> logger, IRoomRepository roomRepository) : base(logger)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IReadOnlyCollection<RoomDto>> BrowseAsync(int buildingId)
        {
            try
            {
                return await _roomRepository.BrowseAsync(buildingId);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return Array.Empty<RoomDto>();
            }
        }

        public async Task<RoomDto?> AddEditAsync(AddEditRoomModel model)
        {
            try
            {
                var entity = (model.Id > 0)
                    ? await _roomRepository.GetOneAsync(x => x.Id == model.Id) ?? new Room()
                    : new Room();

                entity.BuildingId = model.BuildingId;
                entity.MaxPeople = model.MaxPeople;
                entity.BuildingId = model.BuildingId;
                

                await _roomRepository.SaveChangesAsync();
                
                return null;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return new RoomDto()
                {
                    Equipment = model.Equipment.Select(x => new EquipmentDto()
                    {
                        Id = x
                    }).ToList(),
                    Categories = model.Categories.Select(x => new CategoryDto()
                    {
                        Id = x
                    }).ToList(),
                    BuildingId = model.BuildingId,
                    RoomNumber = model.RoomNumber,
                    MaxPeople = model.MaxPeople,
                    Id = model.Id,
                };
            }
        }
    }
}