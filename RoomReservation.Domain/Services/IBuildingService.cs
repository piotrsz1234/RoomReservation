using RoomReservation.Domain.Contracts.Buiding.Dtos;

namespace RoomReservation.Domain.Services {
    public interface IBuildingService {
        Task<IReadOnlyCollection<BuildingDto>> BrowseAsync();
        Task<BuildingDto?> GetOneAsync(int id);
    }
}