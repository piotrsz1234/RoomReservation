using Microsoft.Extensions.Logging;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Contracts.User.Results;
using RoomReservation.Domain.Entities;
using RoomReservation.Domain.Enums;
using RoomReservation.Domain.Helpers;
using RoomReservation.Domain.Repositories;
using RoomReservation.Domain.Services;

namespace RoomReservation.Implementation.Services {
    internal sealed class UserService : ServiceBase, IUserService {
        private readonly IUserRepository _userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository) : base(logger)
        {
            _userRepository = userRepository;
        }

        public async Task<SignInResult> SignInAsync(SignInModel model)
        {
            try
            {
                var hashedPassword = SecurityHelper.HashString(model.Password);
                var user = await _userRepository.GetOneAsync(x => x.IsDeleted == false && x.Email == model.Email
                    && x.Password == hashedPassword);

                if (user is null)
                    return new SignInResult()
                    {
                        Error = "Wrong email or password"
                    };

                return new SignInResult()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                };
            }
            catch (Exception e)
            {
                Logger.LogError(e);

                return new SignInResult()
                {
                    Error = "Unexpected error occured"
                };
            }
        }

        public async Task<SignUpResult> SignUpAsync(SignUpModel model)
        {
            try
            {
                var hashedPassword = SecurityHelper.HashString(model.Password);
                var user = await _userRepository.GetOneAsync(x => x.IsDeleted == false && x.Email == model.Email);

                if (user is not null)
                    return new SignUpResult()
                    {
                        Error = "User with given email already exists"
                    };

                await _userRepository.AddAsync(new User()
                {
                    InsertDateUtc = DateTime.UtcNow,
                    ModificationDateUtc = DateTime.UtcNow,
                    IsDeleted = false,
                    Email = model.Email,
                    Password = hashedPassword,
                    Role = UserRole.User
                });

                await _userRepository.SaveChangesAsync();

                return new SignUpResult();
            }
            catch (Exception e)
            {
                Logger.LogError(e);

                return new SignUpResult()
                {
                    Error = "Unexpected error occured"
                };
            }
        }
    }
}