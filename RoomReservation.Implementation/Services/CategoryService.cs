using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Category.Models;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services {
    internal sealed class CategoryService : ServiceBase, ICategoryService {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository categoryRepository) : base(logger)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IReadOnlyCollection<CategoryDto>> BrowseAsync()
        {
            try
            {
                var result = await _categoryRepository.BrowseAsync();

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                throw;
            }
        }

        public async Task<CategoryDto?> GetOneAsync(int id)
        {
            try
            {
                var result = await _categoryRepository.GetOneAsync(x => x.Id == id && x.IsDeleted == false);

                if (result is null)
                    return null;

                return new CategoryDto()
                {
                    Id = result.Id,
                    Name = result.Name,
                };
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return null;
            }
        }

        public async Task<bool> AddEditAsync(AddEditCategoryModel model)
        {
            try
            {
                var entity = model.Id > 0
                    ? await _categoryRepository.GetOneAsync(x => x.Id == model.Id) ?? new Category()
                    : new Category();

                entity.Name = model.Name;

                await _categoryRepository.AddAsync(entity);

                await _categoryRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            try
            {
                var entity = await _categoryRepository.GetOneAsync(x => x.Id == id);

                if (entity is null)
                    return false;

                await _categoryRepository.RemoveAsync(entity);

                await _categoryRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Logger.LogError(e);
                return false;
            }
        }
    }
}