using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Equipment.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        public async Task<IActionResult> Browse()
        {
            var result = await _equipmentService.BrowseAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id = null)
        {
            if (id is null)
                return View();

            var result = await _equipmentService.GetOneAsync(id.Value);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditEquipmentModel model)
        {
            var result = await _equipmentService.AddEditAsync(model);

            if (!result)
                return RedirectToAction("AddEdit", new { id = model.Id });

            return RedirectToAction("Browse");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _equipmentService.RemoveAsync(id);

            return RedirectToAction("Browse");
        }
    }
}