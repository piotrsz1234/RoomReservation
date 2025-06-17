using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class EquipmentRoom : IEntity
    {
        [Required]
        public int EquipmentId { get; set; }

        [Required]
        public int RoomId { get; set; }

        public virtual Equipment Equipment { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;

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