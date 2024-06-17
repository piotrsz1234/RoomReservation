using RoomReservation.Application.Helpers;
using RoomReservation.Domain;

namespace RoomReservation.Application.Services
{
    public class BaseService
    {
        protected readonly HttpClient Client;
        protected readonly Uri BaseUrl;
        protected SessionHelper SessionHelper;

        public BaseService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper)
        {
            Client = client;
            SessionHelper = sessionHelper;
            BaseUrl = new Uri(configuration.GetConnectionString(Constants.ApiUrlStringName));
        }
    }
}