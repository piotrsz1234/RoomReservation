using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class ReservationRepository : RepositoryGenericBase<Reservation>, IReservationRepository {
        public ReservationRepository(MainDbContext dbContext, ILogger<ReservationRepository> logger) : base(dbContext, logger)
        {
        }
    }
}