using System.ComponentModel.DataAnnotations;
using RoomReservation.Domain.Enums;

namespace RoomReservation.Domain.Entities
{
    public class Reservation : IEntity
    {
        [Required]
        public int UserId { get; set; }

        public int RoomId { get; set; }

        [Required]
        public ReservationType Type { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Duration { get; set; }

        public string? RejectionReason { get; set; }

        public virtual User User { get; set; } = null!;
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