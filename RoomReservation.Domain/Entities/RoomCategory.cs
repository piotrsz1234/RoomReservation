using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities {
    public class RoomCategory : IEntity {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime InsertDateUtc { get; set; }

        [Required]
        public DateTime ModificationDateUtc { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}