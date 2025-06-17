using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts;
using RoomReservation.Domain.Contracts.Buiding.Dtos;
using RoomReservation.Domain.Contracts.Buiding.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<BuildingDto>> Browse()
        {
            var result = await _buildingService.BrowseAsync();

            return result;
        }

        [HttpGet]
        public async Task<BuildingDto?> GetOne(int id)
        {
            var result = await _buildingService.GetOneAsync(id);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditBuildingModel model)
        {
            var result = await _buildingService.AddEditAsync(model);

            if (result is null)
                return StatusCode(500);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(RemoveModel model)
        {
            await _buildingService.RemoveAsync(model.Id);

            return Ok(true);
        }
    }
}