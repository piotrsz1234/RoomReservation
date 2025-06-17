using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services
{
    internal sealed class ReservationService : ServiceBase, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository, ILogger<ReservationService> logger) :
            base(logger)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IReadOnlyCollection<ReservationDto>> GetUsersReservationsAsync(int userId)
        {
            try
            {
                return await _reservationRepository.GetUsersAsync(userId);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return Array.Empty<ReservationDto>();
            }
        }

        public async Task<ReservationDto?> ReserveAsync(ReservationDto model, int userId)
        {
            try
            {
                if ((model.StartDate - DateTime.UtcNow).TotalDays >= 365 * 2)
                {
                    model.Error = "You cannot reserve more than 2 years ahead";
                    return model;
                }

                if (model.StartDate < DateTime.UtcNow)
                {
                    model.Error = "Time is oneway :)";
                    return model;
                }

                if (model.Duration <= 0)
                {
                    model.Error = "Duration must be more than 1 minute";
                    return model;
                }

                var isReserved = await _reservationRepository.IsRoomFreeAsync(model.RoomId, model.StartDate,
                    model.StartDate.AddMinutes(model.Duration));

                if (isReserved)
                {
                    model.Error = "Room already reserved";
                    return model;
                }

                var reservation = new Reservation
                {
                    RoomId = model.RoomId,
                    Duration = model.Duration,
                    StartDate = model.StartDate,
                    Type = ReservationType.OneTime,
                    IsConfirmed = true,
                    UserId = userId,
                    InsertDateUtc = DateTime.UtcNow,
                    ModificationDateUtc = DateTime.UtcNow
                };

                await _reservationRepository.AddAsync(reservation);

                await _reservationRepository.SaveChangesAsync();

                return null;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                model.Error = "Unexpected error";
                return model;
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var entity = await _reservationRepository.GetOneAsync(id);

                if (entity is null)
                    return;

                entity.IsDeleted = true;
                entity.ModificationDateUtc = DateTime.UtcNow;

                await _reservationRepository.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }
    }
}