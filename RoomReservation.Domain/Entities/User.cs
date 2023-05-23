﻿using System.ComponentModel.DataAnnotations;
using RoomReservation.Domain.Enums;

namespace RoomReservation.Domain.Entities {
    public class User : IEntity {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime InsertDateUtc { get; set; }
        [Required]
        public DateTime ModificationDateUtc { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; }
    }
}