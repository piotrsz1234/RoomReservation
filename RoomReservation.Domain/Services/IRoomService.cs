using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Contracts.Room.Models;

namespace RoomReservation.Domain.Services {
    public interface IRoomService {
        Task<IReadOnlyCollection<RoomDto>> BrowseAsync(int buildingId);
        Task<RoomDto?> AddEditAsync(AddEditRoomModel model);
        Task<RoomDto?> GetOneAsync(int id);
    }
}