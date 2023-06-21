using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Room.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers {
    public class RoomController : Controller {
        private readonly IRoomService _roomService;


        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Browse(int buildingId)
        {
            var data = await _roomService.BrowseAsync(buildingId);
            
            return View(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> AddEdit()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditRoomModel model)
        {
            var result = await _roomService.AddEditAsync(model);

            return View();
        }
    }
}