using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Domain.Helpers;
using RoomReservation.Implementation.DbContexts;

namespace RoomReservation.Implementation.Services
{
    public class SeedingService
    {
        public static void Seed(MainDbContext context)
        {
            var adminUser = new User
            {
                Email = "admin@g.com",
                Password = SecurityHelper.HashString("test"),
                InsertDateUtc = DateTime.UtcNow,
                ModificationDateUtc = DateTime.UtcNow,
                Role = UserRole.Admin
            };

            if (!context.User.Any(x => x.Email == adminUser.Email))
                context.User.Add(adminUser);

            context.SaveChanges();
        }
    }
}