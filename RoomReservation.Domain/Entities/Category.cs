using System.ComponentModel.DataAnnotations;

namespace RoomReservation.Domain.Entities
{
    public class Category : IEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<RoomCategory> RoomCategories { get; set; } = new HashSet<RoomCategory>();

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