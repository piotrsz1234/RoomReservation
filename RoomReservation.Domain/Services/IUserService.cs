using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Contracts.User.Results;

namespace RoomReservation.Domain.Services {
    public interface IUserService {
        Task<SignInResult> SignInAsync(SignInModel model);
        Task<SignUpResult> SignUpAsync(SignUpModel model);
    }
}