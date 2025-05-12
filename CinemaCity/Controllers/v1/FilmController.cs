using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using CinemaCity.Services.Abstract;
using CinemaCity.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/film")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;
        public FilmController(IFilmService filmService, IUserService userService, IBasketService basketService)
        {
            _filmService = filmService;
            _userService = userService;
            _basketService = basketService;
        }
        [HttpGet("allFilms")]
        public async Task<IActionResult> GetAll()
        {
            var films = await _filmService.GetAllAsync();
            return Ok(films);
        }
        
    }
}
