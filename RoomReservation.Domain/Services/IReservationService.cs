using RoomReservation.Domain.Contracts.Reservation.Dtos;

namespace RoomReservation.Domain.Services
{
    public interface IReservationService
    {
        Task<IReadOnlyCollection<ReservationDto>> GetUsersReservationsAsync(int userId);
    }
}