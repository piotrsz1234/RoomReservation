using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Application.Helpers;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers
{
    [Authorize]
    public class ReservationController: Controller
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
    }
}