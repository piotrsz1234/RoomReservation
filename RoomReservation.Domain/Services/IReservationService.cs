using RoomReservation.Domain.Contracts.Reservation.Dtos;

namespace RoomReservation.Domain.Services
{
    public interface IReservationService
    {
        Task<IReadOnlyCollection<ReservationDto>> GetUsersReservationsAsync(int userId);
        Task<ReservationDto?> ReserveAsync(ReservationDto model, int userId);
        Task RemoveAsync(int id);
    }
}