using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Contracts.Buiding.Models;

namespace RoomReservation.Domain.Services {
    public interface IBuildingService {
        Task<IReadOnlyCollection<BuildingDto>> BrowseAsync();
        Task<BuildingDto?> GetOneAsync(int id);
        Task<BuildingDto?> AddEditAsync(AddEditBuildingModel model);
        Task RemoveAsync(int id);
    }
}