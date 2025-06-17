using Flurl;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Equipment.Dtos;
using RoomReservation.Domain.Contracts.Equipment.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class EquipmentService : BaseService, IEquipmentService
    {
        public EquipmentService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<IReadOnlyCollection<EquipmentDto>> BrowseAsync()
        {
            return await Client.GetCall<IReadOnlyCollection<EquipmentDto>>(new Uri(BaseUrl, "Equipment/Browse"));
        }

        public async Task<EquipmentDto?> GetOneAsync(int id)
        {
            return await Client.GetCall<EquipmentDto?>(new Uri(BaseUrl, "Equipment/GetOne").SetQueryParam("id", id).ToUri());
        }

        public async Task<bool> AddEditAsync(AddEditEquipmentModel model)
        {
            return await Client.PostCall<bool, AddEditEquipmentModel>(new Uri(BaseUrl, "Equipment/AddEdit"), model);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await Client.PostCall<RemoveModel>(new Uri(BaseUrl, "Equipment/Remove"), new RemoveModel { Id = id });
        }
    }
}