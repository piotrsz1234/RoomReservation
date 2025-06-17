using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class Building : IEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string BuildingNumber { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string PostalCode { get; set; } = string.Empty;

        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

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