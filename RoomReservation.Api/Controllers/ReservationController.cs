using Microsoft.AspNetCore.Mvc;
using RoomReservation.Api.Middleware;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Reservation.Dtos;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers
{
    [BasicAuthenticationFilter]
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly SessionHelper _sessionHelper;

        public ReservationController(IReservationService reservationService, SessionHelper sessionHelper)
        {
            _reservationService = reservationService;
            _sessionHelper = sessionHelper;
        }

        [HttpGet]
        public async Task<ActionResult> Browse()
        {
            var reservations = await _reservationService.GetUsersReservationsAsync(_sessionHelper.UserId.Value);

            return Ok(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReservationDto model)
        {
            var result = await _reservationService.ReserveAsync(model, _sessionHelper.UserId!.Value);

            if (result is null)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(RemoveModel model)
        {
            await _reservationService.RemoveAsync(model.Id);

            return Ok();
        }
    }
}