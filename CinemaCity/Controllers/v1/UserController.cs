﻿using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using CinemaCity.Services.Abstract;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCity.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _userService.RegisterUserAsync(request);
            return Ok(new { success = true, token });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { success = false, message = "Email and password are required." });
            }
            var response = await _userService.LoginUserAsync(request);
            if (string.IsNullOrEmpty(response))
            {
                return Unauthorized(new { success = false, message = "Invalid credentials" });
            }
            return Ok(new { success = true, response });
        }
    }
}
