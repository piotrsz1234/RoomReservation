using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Buiding.Dtos;
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
                    Address = result.Address,
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
    }
}