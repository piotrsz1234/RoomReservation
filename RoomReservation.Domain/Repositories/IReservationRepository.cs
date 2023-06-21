using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories {
    public interface IReservationRepository : IRepositoryGenericBase<Reservation> {
        Task<IReadOnlyCollection<ReservationDto>> GetUsersAsync(int userId);
        Task<bool> IsRoomFreeAsync(int roomId, DateTime startDate, DateTime endDate);
    }
}