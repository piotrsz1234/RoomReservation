﻿namespace RoomReservation.Domain.Contracts.Buiding.Models
{
    public class AddEditBuildingModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}