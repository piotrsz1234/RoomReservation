using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Models;

namespace RoomReservation.Domain.Services
{
    public interface IEquipmentService
    {
        Task<IReadOnlyCollection<EquipmentDto>> BrowseAsync();
        Task<EquipmentDto?> GetOneAsync(int id);
        Task<bool> AddEditAsync(AddEditEquipmentModel model);
        Task<bool> RemoveAsync(int id);
    }
}