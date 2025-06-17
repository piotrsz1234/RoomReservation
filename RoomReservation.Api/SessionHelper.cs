using RoomReservation.Domain;

namespace RoomReservation.Api
{
    public sealed class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

        public int? UserId
        {
            get
            {
                if (HttpContext?.User?.Identity?.IsAuthenticated != true)
                    return null;

                return int.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.UserIdClaimType)?.Value, out var id)
                    ? id
                    : null;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (HttpContext?.User?.Identity?.IsAuthenticated != true)
                    return false;

                return bool.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == Constants.IsAdminClaimType)?.Value, out var id) &&
                       id;
            }
        }
    }
}