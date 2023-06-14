using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers {
    public class BuildingController : Controller {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var result = await _buildingService.BrowseAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id = null)
        {
            if (id is null)
                return View();

            var building = await _buildingService.GetOneAsync(id.Value);

            return View(building);
        }
    }
}