using RoomReservation.Domain.Enums;

namespace RoomReservation.Domain.Contracts.User.Results
{
    public sealed class SignInResult
    {
        public string? Error { get; set; } = null;
        public int? UserId { get; set; } = null;
        public string? Email { get; set; } = null;
        public UserRole? Role { get; set; } = null;
    }
}