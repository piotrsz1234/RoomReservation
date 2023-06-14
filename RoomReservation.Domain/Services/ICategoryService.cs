using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Category.Models;

namespace RoomReservation.Domain.Services {
    public interface ICategoryService {
        Task<IReadOnlyCollection<CategoryDto>> BrowseAsync();
        Task<CategoryDto?> GetOneAsync(int id);
        Task<bool> AddEditAsync(AddEditCategoryModel model);
        Task<bool> RemoveAsync(int id);
    }
}