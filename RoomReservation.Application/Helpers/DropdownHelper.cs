using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservation.Domain.Services;

namespace RoomReservation.Application.Helpers
{
    public class DropdownHelper
    {
        private readonly ICategoryService _categoryService;
        private readonly IEquipmentService _equipmentService;

        public DropdownHelper(ICategoryService categoryService, IEquipmentService equipmentService)
        {
            _categoryService = categoryService;
            _equipmentService = equipmentService;
        }

        public async Task<IReadOnlyCollection<SelectListItem>> GetCategories()
        {
            var data = await _categoryService.BrowseAsync();

            return data.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();
        }

        public async Task<IReadOnlyCollection<SelectListItem>> GetEquipment()
        {
            var data = await _equipmentService.BrowseAsync();

            return data.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToArray();
        }
    }
}