using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Contracts.Buiding.Models;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services {
    internal sealed class BuildingService : ServiceBase, IBuildingService {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingService(ILogger<BuildingService> logger, IBuildingRepository buildingRepository) : base(logger)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<IReadOnlyCollection<BuildingDto>> BrowseAsync()
        {
            try
            {
                var result = await _buildingRepository.BrowseAsync();

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return Array.Empty<BuildingDto>();
            }
        }

        public async Task<BuildingDto?> GetOneAsync(int id)
        {
            try
            {
                var result = await _buildingRepository.GetOneAsync(x => x.Id == id && x.IsDeleted == false);

                if (result is null)
                    return null;

                return new BuildingDto()
                {
                    Id = result.Id,
                    Street = result.Street,
                    BuildingNumber =result.BuildingNumber,
                    City = result.City,
                    Name = result.Name,
                    PostalCode = result.PostalCode,
                };
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }

        public async Task<BuildingDto?> AddEditAsync(AddEditBuildingModel model)
        {
            try
            {
                var entity = model.Id > 0
                    ? await _buildingRepository.GetOneAsync(x => x.Id == model.Id) ?? new Building()
                    : new Building();

                entity.Name = model.Name;
                entity.Street = model.Street;
                entity.BuildingNumber = model.BuildingNumber;
                entity.City = model.City;
                entity.PostalCode = model.PostalCode;
                entity.ModificationDateUtc = DateTime.UtcNow;

                await _buildingRepository.AddAsync(entity);
                
                await _buildingRepository.SaveChangesAsync();

                return null;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return new BuildingDto()
                {
                    Id = model.Id,
                    Street = model.Street,
                    BuildingNumber = model.BuildingNumber,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    Name = model.Name,
                };
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await _buildingRepository.GetOneAsync(id);

                if (entity is null)
                    return;

                entity.IsDeleted = true;
                entity.ModificationDateUtc = DateTime.UtcNow;

                await _buildingRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}