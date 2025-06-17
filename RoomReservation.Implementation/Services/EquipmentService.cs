using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Models;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services
{
    internal sealed class EquipmentService : ServiceBase, IEquipmentService
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentService(ILogger<CategoryService> logger, IEquipmentRepository equipmentRepository) : base(logger)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<IReadOnlyCollection<EquipmentDto>> BrowseAsync()
        {
            try
            {
                var result = await _equipmentRepository.BrowseAsync();

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<EquipmentDto?> GetOneAsync(int id)
        {
            try
            {
                var result = await _equipmentRepository.GetOneAsync(x => x.Id == id && x.IsDeleted == false);

                if (result is null)
                    return null;

                return new EquipmentDto
                {
                    Id = result.Id,
                    Name = result.Name
                };
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }

        public async Task<bool> AddEditAsync(AddEditEquipmentModel model)
        {
            try
            {
                var entity = model.Id > 0
                    ? await _equipmentRepository.GetOneAsync(x => x.Id == model.Id) ?? new Equipment()
                    : new Equipment();

                entity.Name = model.Name;

                await _equipmentRepository.AddAsync(entity);

                await _equipmentRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var entity = await _equipmentRepository.GetOneAsync(x => x.Id == id);

                if (entity is null)
                    return false;

                await _equipmentRepository.RemoveAsync(entity);

                await _equipmentRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }
    }
}