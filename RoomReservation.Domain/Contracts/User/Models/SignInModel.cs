namespace RoomReservation.Domain.Contracts.User.Models {
    public sealed class SignInModel {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}