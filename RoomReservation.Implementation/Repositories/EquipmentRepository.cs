using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class EquipmentRepository : RepositoryGenericBase<Equipment>, IEquipmentRepository {
        public EquipmentRepository(MainDbContext dbContext, ILogger<EquipmentRepository> logger) : base(dbContext, logger)
        {
        }
    }
}