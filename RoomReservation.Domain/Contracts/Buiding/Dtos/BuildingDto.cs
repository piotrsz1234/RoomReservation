﻿namespace RoomReservation.Domain.Contracts.Buiding.Dtos {
    public sealed class BuildingDto {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;
        
        public string City { get; set; } = string.Empty;
        
        public string PostalCode { get; set; } = string.Empty;
    }
}