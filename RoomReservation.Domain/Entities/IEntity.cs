namespace RoomReservation.Domain.Entities {
    public interface IEntity {
        public int Id { get; set; }
        public DateTime InsertDateUtc { get; set; }
        public DateTime ModificationDateUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}