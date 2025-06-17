using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class Equipment : IEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<EquipmentRoom> EquipmentRooms { get; set; } = new HashSet<EquipmentRoom>();

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