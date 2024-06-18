using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain;
using RoomReservation.Domain.Contracts.User.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers {
    public class UserController : Controller {
        private readonly IUserService _userService;
        private readonly SessionHelper _sessionHelper;


        public UserController(IUserService userService, SessionHelper sessionHelper)
        {
            _userService = userService;
            _sessionHelper = sessionHelper;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            try
            {
                var result = await _userService.SignInAsync(model);

                if (!string.IsNullOrWhiteSpace(result.Error) || !result.UserId.HasValue)
                {
                    return View(result);
                }
                
                var identity = new ClaimsIdentity(new[]
                    { new Claim(Constants.UserIdClaimType, result.UserId.Value.ToString()) }, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                _sessionHelper.User = result;

                _sessionHelper.SignInModel = model;
                
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            try
            {
                var result = await _userService.SignUpAsync(model);

                if (!string.IsNullOrWhiteSpace(result?.Error))
                    return View(result);

                return RedirectToAction("SignIn");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}