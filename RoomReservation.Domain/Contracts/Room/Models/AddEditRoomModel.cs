namespace RoomReservation.Domain.Contracts.Room.Models
{
    public class AddEditRoomModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int MaxPeople { get; set; }
        public IReadOnlyCollection<int> Categories { get; set; } = Array.Empty<int>();
        public IReadOnlyCollection<int> Equipment { get; set; } = Array.Empty<int>();
        public int BuildingId { get; set; }
    }
}