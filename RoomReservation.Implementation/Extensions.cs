using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoomReservation.Domain;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;
using RoomReservation.Implementation.DbContexts;
using RoomReservation.Implementation.Repositories;
using RoomReservation.Implementation.Services;

namespace RoomReservation.Implementation
{
    public static class Extensions
    {
        public static IServiceCollection AddRoomReservationImplementation(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IEquipmentRoomRepository, EquipmentRoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IRoomCategoryRepository, RoomCategoryRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddDbContext<MainDbContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString(Constants
                    .MainDbContextConnectionStringNameMainDbContextConnectionStringName));
            });

            return services;
        }
    }
}