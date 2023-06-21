using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories {
    public interface IRoomRepository : IRepositoryGenericBase<Room> {
        Task<IReadOnlyCollection<RoomDto>> BrowseAsync(int buildingId);
        Task<RoomDto?> GetOneDto(int id);
    }
}