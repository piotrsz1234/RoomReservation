using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories {
    public interface IBuildingRepository : IRepositoryGenericBase<Building> {
        Task<IReadOnlyCollection<BuildingDto>> BrowseAsync();
    }
}