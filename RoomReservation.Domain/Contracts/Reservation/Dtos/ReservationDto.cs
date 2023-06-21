﻿namespace RoomReservation.Domain.Contracts.Reservation.Dtos
{
    public class ReservationDto
    {
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string BuildingName { get; set; } = string.Empty;
    }
}