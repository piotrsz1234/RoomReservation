using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Room.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Browse(int buildingId)
        {
            var data = await _roomService.BrowseAsync(buildingId);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetOne(int id)
        {
            var data = await _roomService.GetOneAsync(id);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit([FromBody] AddEditRoomModel model)
        {
            var result = await _roomService.AddEditAsync(model);

            if (result is not null)
                return StatusCode(500, result);

            return Ok(result);
        }
    }
}