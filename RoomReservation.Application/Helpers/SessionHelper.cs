using Newtonsoft.Json;
using RoomReservation.Domain.Contracts.User.Results;

namespace RoomReservation.Application.Helpers {
    public sealed class SessionHelper {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public SignInResult? User
        {
            get => JsonConvert.DeserializeObject<SignInResult>(_httpContextAccessor.HttpContext.Session.GetString("User") ?? string.Empty);
            set => _httpContextAccessor.HttpContext.Session.SetString("User", JsonConvert.SerializeObject(value));
        }
    }
}