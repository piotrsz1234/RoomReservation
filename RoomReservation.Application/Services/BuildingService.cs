using Flurl;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Contracts.Buiding.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class BuildingService : BaseService, IBuildingService
    {
        public BuildingService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<IReadOnlyCollection<BuildingDto>> BrowseAsync()
        {
            return await Client.GetCall<IReadOnlyCollection<BuildingDto>>(new Uri(BaseUrl, "Building/Browse"));
        }

        public async Task<BuildingDto?> GetOneAsync(int id)
        {
            return await Client.GetCall<BuildingDto?>(new Uri(BaseUrl, "Building/GetOne").SetQueryParam("id", id).ToUri());
        }

        public async Task<BuildingDto?> AddEditAsync(AddEditBuildingModel model)
        {
            return await Client.PostCall<BuildingDto?, AddEditBuildingModel>(new Uri(BaseUrl, "Building/AddEdit"), model);
        }

        public async Task RemoveAsync(int id)
        {
            await Client.PostCall<RemoveModel>(new Uri(BaseUrl, "Building/Remove"), new RemoveModel { Id = id });
        }
    }
}