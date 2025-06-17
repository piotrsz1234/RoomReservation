using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories
{
    public interface IEquipmentRepository : IRepositoryGenericBase<Equipment>
    {
        Task<IReadOnlyCollection<EquipmentDto>> BrowseAsync();
    }
}