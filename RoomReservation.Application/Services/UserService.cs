using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Contracts.User.Results;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(HttpClient client, IConfiguration configuration, SessionHelper sessionHelper) : base(client, configuration, sessionHelper)
        {
        }

        public async Task<SignInResult> SignInAsync(SignInModel model)
        {
            return await Client.PostCall<SignInResult, SignInModel>(new Uri(BaseUrl, "User/SignIn"), model);
        }

        public async Task<SignUpResult> SignUpAsync(SignUpModel model)
        {
            return await Client.PostCall<SignUpResult, SignUpModel>(new Uri(BaseUrl, "User/SignUp"), model);
        }
    }
}