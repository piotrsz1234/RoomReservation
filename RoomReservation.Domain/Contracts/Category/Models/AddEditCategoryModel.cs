namespace RoomReservation.Domain.Contracts.Category.Models {
    public sealed class AddEditCategoryModel {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}