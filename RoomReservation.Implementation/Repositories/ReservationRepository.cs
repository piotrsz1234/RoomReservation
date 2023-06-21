using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories
{
    public class ReservationRepository : RepositoryGenericBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(MainDbContext dbContext, ILogger<ReservationRepository> logger) : base(dbContext, logger)
        {
        }

        public async Task<IReadOnlyCollection<ReservationDto>> GetUsersAsync(int userId)
        {
            try {
                return await DbContext.Reservation.Where(x => x.UserId == userId && x.IsDeleted == false && x.StartDate >= DateTime.UtcNow)
                    .Select(x => new ReservationDto() {
                            StartDate = x.StartDate,
                            Duration = x.Duration,
                            BuildingName = x.Room.Building.Name,
                            RoomNumber = x.Room.RoomNumber
                        }).ToListAsync();
            } catch (Exception e) {
                Logger.LogError(e);
                throw;
            }
        }
    }
}