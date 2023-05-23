using Microsoft.Extensions.Logging;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Repositories {
    public class UserRepository : RepositoryGenericBase<User>, IUserRepository {
        public UserRepository(MainDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext, logger)
        {
        }
    }
}