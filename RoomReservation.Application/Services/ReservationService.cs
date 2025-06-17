using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class ReservationService : BaseService, IReservationService
    {
        public ReservationService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<IReadOnlyCollection<ReservationDto>> GetUsersReservationsAsync(int userId)
        {
            return await Client.GetCall<IReadOnlyCollection<ReservationDto>>(new Uri(BaseUrl, "Reservation/Browse"));
        }

        public async Task<ReservationDto?> ReserveAsync(ReservationDto model, int userId)
        {
            return await Client.PostCall<ReservationDto?, ReservationDto>(new Uri(BaseUrl, "Reservation/Reserve"), model);
        }

        public async Task RemoveAsync(int id)
        {
            await Client.PostCall<RemoveModel>(new Uri(BaseUrl, "Reservation/Remove"), new RemoveModel { Id = id });
        }
    }
}