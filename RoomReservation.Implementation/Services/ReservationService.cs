using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services
{
    internal sealed class ReservationService : ServiceBase, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository, ILogger<ReservationService> logger) : base(logger)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IReadOnlyCollection<ReservationDto>> GetUsersReservationsAsync(int userId)
        {
            try {
                return await _reservationRepository.GetUsersAsync(userId);
            } catch (Exception e) {
                Logger.LogError(e);
                return Array.Empty<ReservationDto>();
            }
        }
    }
}