using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class Room : IEntity
    {
        [Required]
        public int BuildingId { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public int MaxPeople { get; set; }

        public virtual ICollection<RoomCategory> RoomCategories { get; set; } = new HashSet<RoomCategory>();
        public virtual ICollection<EquipmentRoom> EquipmentRooms { get; set; } = new HashSet<EquipmentRoom>();
        public virtual Building Building { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime InsertDateUtc { get; set; }

        [Required]
        public DateTime ModificationDateUtc { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}