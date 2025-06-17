using Flurl;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Category.Dtos;
using RoomReservation.Domain.Contracts.Category.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<IReadOnlyCollection<CategoryDto>> BrowseAsync()
        {
            return await Client.GetCall<IReadOnlyCollection<CategoryDto>>(new Uri(BaseUrl, "Category/Browse"));
        }

        public async Task<CategoryDto?> GetOneAsync(int id)
        {
            return await Client.GetCall<CategoryDto?>(new Uri(BaseUrl, "Building/GetOne").SetQueryParam("id", id).ToUri());
        }

        public async Task<bool> AddEditAsync(AddEditCategoryModel model)
        {
            return await Client.PostCall<bool, AddEditCategoryModel>(new Uri(BaseUrl, "Category/AddEdit"), model);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await Client.PostCall<RemoveModel>(new Uri(BaseUrl, "Building/Remove"), new RemoveModel { Id = id });
        }
    }
}