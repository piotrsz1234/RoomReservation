using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Room.Dtos;
using RoomReservation.Domain.Contracts.Room.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers
{
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

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id = null)
        {
            RoomDto? room = null;
            if (id is not null) room = await _roomService.GetOneAsync(id.Value);
            return View(room ?? new RoomDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditRoomModel model)
        {
            model.Categories = Request.Form.Where(x => x.Key == "Categories").SelectMany(x => x.Value.ToString().Split(',')).Select(x => int.Parse(x)).ToArray();
            model.Equipment = Request.Form.Where(x => x.Key == "Equipment").SelectMany(x => x.Value.ToString().Split(',')).Select(x => int.Parse(x)).ToArray();
            var result = await _roomService.AddEditAsync(model);

            if (result is null)
                return RedirectToAction("Browse", new { buildingId = model.BuildingId });

            return View(result);
        }
    }
}