using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Domain.Contracts.Category.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Browse()
        {
            var result = await _categoryService.BrowseAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id = null)
        {
            if (id is null)
                return View();

            var result = await _categoryService.GetOneAsync(id.Value);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditCategoryModel model)
        {
            var result = await _categoryService.AddEditAsync(model);

            if (!result)
                return RedirectToAction("AddEdit", new { id = model.Id });

            return RedirectToAction("Browse");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _categoryService.RemoveAsync(id);

            return RedirectToAction("Browse");
        }
    }
}