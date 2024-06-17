using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomReservation.Api.Middleware;
using RoomReservation.Domain.Contracts.Category.Models;
using RoomReservation.Domain.Services;

namespace RoomReservation.Api.Controllers {
    [BasicAuthenticationFilter]
    [Route("[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Browse()
        {
            var result = await _categoryService.BrowseAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(AddEditCategoryModel model)
        {
            var result = await _categoryService.AddEditAsync(model);

            if (!result)
                return StatusCode(500);

            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            await _categoryService.RemoveAsync(id);

            return Ok(true);
        }
    }
}