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
                    ? await _roomRepository.GetOneAsync(x => x.Id == model.Id, x => x.EquipmentRooms, x => x.RoomCategories) ?? new Room()
                    : new Room();

                entity.BuildingId = model.BuildingId;
                entity.MaxPeople = model.MaxPeople;
                entity.RoomNumber = model.RoomNumber;
                entity.BuildingId = model.BuildingId;

                foreach (var item in entity.EquipmentRooms) {
                    item.IsDeleted = true;
                    item.ModificationDateUtc = DateTime.UtcNow;
                }
                
                foreach (var item in entity.RoomCategories) {
                    item.IsDeleted = true;
                    item.ModificationDateUtc = DateTime.UtcNow;
                }

                foreach (var item in model.Categories) {
                    entity.RoomCategories.Add(new RoomCategory() {
                        CategoryId = item,
                    });
                }
                
                foreach (var item in model.Equipment) {
                    entity.EquipmentRooms.Add(new EquipmentRoom() {
                        EquipmentId = item,
                    });
                }

                await _roomRepository.AddAsync(entity);
                
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

        public async Task<RoomDto?> GetOneAsync(int id)
        {
            try {
                var entity = await _roomRepository.GetOneDto(id);

                return entity;
            } catch (Exception e) {
                Logger.LogError(e);
                throw;
            }
        }
    }
}