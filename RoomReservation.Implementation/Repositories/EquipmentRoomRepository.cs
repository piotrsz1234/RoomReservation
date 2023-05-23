using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class EquipmentRoomRepository : RepositoryGenericBase<EquipmentRoom>, IEquipmentRoomRepository {
        public EquipmentRoomRepository(MainDbContext dbContext, ILogger<EquipmentRoomRepository> logger) : base(dbContext, logger)
        {
        }
    }
}