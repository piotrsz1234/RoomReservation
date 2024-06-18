using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers {
    
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller {
        private readonly IUserService _userService;
        private readonly SessionHelper _sessionHelper;


        public UserController(IUserService userService, SessionHelper sessionHelper)
        {
            _userService = userService;
            _sessionHelper = sessionHelper;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel model)
        {
            try
            {
                var result = await _userService.SignUpAsync(model);

                if (!string.IsNullOrWhiteSpace(result.Error))
                    return StatusCode(400, result);

                return Ok(null);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignInModel model)
        {
            try
            {
                var result = await _userService.SignInAsync(model);

                if (!string.IsNullOrWhiteSpace(result.Error))
                    return StatusCode(400, result);

                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}