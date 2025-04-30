using CinemaCity.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CinemaCity.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/film")]
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }
        [HttpGet("allFilms")]
        public async Task<IActionResult> GetAll()
        {
            var films = await _filmService.GetAllAsync();
            return Ok(films);
        }
    }
}
