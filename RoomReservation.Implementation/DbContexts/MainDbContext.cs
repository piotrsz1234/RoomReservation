using Microsoft.EntityFrameworkCore;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Implementation.DbContexts {
    public class MainDbContext : DbContext {
        private const string DefaultConnectionString = "Data Source=.;Database=RoomReservation;Trusted_Connection=True;Trust Server Certificate=true";

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Building> Building { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Equipment> Equipment { get; set; } = default!;
        public DbSet<EquipmentRoom> EquipmentRoom { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; } = default!;
        public DbSet<Room> Room { get; set; } = default!;
        public DbSet<RoomCategory> RoomCategory { get; set; } = default!;

        internal MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EquipmentRoom>(builder =>
            {
                builder.HasOne(e => e.Equipment).WithMany(e => e.EquipmentRooms).HasForeignKey(e => e.EquipmentId);
                builder.HasOne(e => e.Room).WithMany(e => e.EquipmentRooms).HasForeignKey(e => e.RoomId);
            });

            modelBuilder.Entity<Room>(builder =>
            {
                builder.HasOne(e => e.Building).WithMany(e => e.Rooms).HasForeignKey(e => e.BuildingId);
            });

            modelBuilder.Entity<RoomCategory>(builder =>
            {
                builder.HasOne(e => e.Category).WithMany(e => e.RoomCategories).HasForeignKey(e => e.CategoryId);
                builder.HasOne(e => e.Room).WithMany(e => e.RoomCategories).HasForeignKey(e => e.RoomId);
            });

            modelBuilder.Entity<Reservation>(builder =>
            {
                builder.HasOne(e => e.User).WithMany(e => e.Reservations).HasForeignKey(e => e.UserId);
                builder.HasOne(e => e.Room).WithMany(e => e.Reservations).HasForeignKey(e => e.RoomId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DefaultConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}