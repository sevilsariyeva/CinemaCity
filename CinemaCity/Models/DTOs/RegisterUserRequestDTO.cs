﻿namespace CinemaCity.Models.DTOs
{
    public class RegisterUserRequestDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImageUrl { get; set; }
    }
}
