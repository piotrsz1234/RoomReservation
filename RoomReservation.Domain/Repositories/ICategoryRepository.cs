using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Entities;

namespace RoomReservation.Domain.Repositories {
    public interface ICategoryRepository : IRepositoryGenericBase<Category> {
        Task<IReadOnlyCollection<CategoryDto>> BrowseAsync();
    }
}