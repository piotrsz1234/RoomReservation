using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Equipment.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers {
    [ApiController]
    [Route("[controller]/[action]")]
    public class EquipmentController : Controller {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var result = await _equipmentService.BrowseAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditEquipmentModel model)
        {
            var result = await _equipmentService.AddEditAsync(model);

            if (!result)
                return StatusCode(500);

            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _equipmentService.RemoveAsync(id);

            if (!result)
                return StatusCode(500);
            
            return Ok(true);
        }
    }
}