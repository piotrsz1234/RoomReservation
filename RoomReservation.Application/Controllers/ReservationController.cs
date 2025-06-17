using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly SessionHelper _sessionHelper;

        public ReservationController(IReservationService reservationService, SessionHelper sessionHelper)
        {
            _reservationService = reservationService;
            _sessionHelper = sessionHelper;
        }

        public async Task<ActionResult> Browse()
        {
            var reservations = await _reservationService.GetUsersReservationsAsync(_sessionHelper.User!.UserId!.Value);

            return View(reservations);
        }

        [HttpGet]
        public IActionResult Reserve()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReservationDto model)
        {
            var result = await _reservationService.ReserveAsync(model, _sessionHelper.User!.UserId!.Value);

            if (result is null)
                return RedirectToAction("Browse");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _reservationService.RemoveAsync(id);

            return RedirectToAction("Browse");
        }
    }
}