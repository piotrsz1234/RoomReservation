using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Dtos;

namespace RoomReservation.Domain.Contracts.Room.Dtos
{
    public sealed class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int MaxPeople { get; set; }
        public IReadOnlyCollection<CategoryDto> Categories { get; set; } = Array.Empty<CategoryDto>();
        public IReadOnlyCollection<EquipmentDto> Equipment { get; set; } = Array.Empty<EquipmentDto>();
        public int BuildingId { get; set; }
    }
}