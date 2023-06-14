using Newtonsoft.Json;
using RoomReservation.Domain.Contracts.User.Results;
using RoomReservation.Domain.Enums;

namespace RoomReservation.Application.Helpers {
    public sealed class SessionHelper {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;
        
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public SignInResult? User
        {
            get
            {
                if(HttpContext?.User?.Identity?.IsAuthenticated == false)
                    HttpContext.Session.Clear();

                return JsonConvert.DeserializeObject<SignInResult>(
                    HttpContext?.Session.GetString("User") ?? string.Empty);
            }
            set
            {
                HttpContext?.Session.SetString("User", JsonConvert.SerializeObject(value));
                HttpContext?.Session.SetInt32("IsAdmin", value?.Role == UserRole.Admin ? 1: 0);
            }
        }

        public bool IsAdmin => HttpContext?.Session.GetInt32("IsAdmin") == 1;
    }
}