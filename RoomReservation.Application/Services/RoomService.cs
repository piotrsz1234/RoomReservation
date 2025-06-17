using Flurl;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Contracts.Room.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class RoomService : BaseService, IRoomService
    {
        public RoomService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<IReadOnlyCollection<RoomDto>> BrowseAsync(int buildingId)
        {
            return await Client.GetCall<IReadOnlyCollection<RoomDto>>(new Uri(BaseUrl, "Room/Browse").SetQueryParam("buildingId", buildingId).ToUri());
        }

        public async Task<RoomDto?> AddEditAsync(AddEditRoomModel model)
        {
            return await Client.PostCall<RoomDto?, AddEditRoomModel>(new Uri(BaseUrl, "Room/AddEdit"), model);
        }

        public async Task<RoomDto?> GetOneAsync(int id)
        {
            return await Client.GetCall<RoomDto?>(new Uri(BaseUrl, "Room/GetOne").SetQueryParam("id", id).ToUri());
        }
    }
}