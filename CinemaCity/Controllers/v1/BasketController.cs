using CinemaCity.Models.DTOs;
using CinemaCity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/basket")]
    public class BasketController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFilmService _filmService;
        private readonly IBasketService _basketService;

        public BasketController(IUserService userService, IFilmService filmService, IBasketService basketService)
        {
            _userService = userService;
            _filmService = filmService;
            _basketService = basketService;
        }
        [Authorize]
        [HttpPost("addToBasket")]
        public async Task<IActionResult> AddToBasket([FromBody] AddToBasketRequestDTO request)
        {
            int? userId = _userService.GetUserIdFromToken(HttpContext.User);
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                await _basketService.AddTicketToBasketAsync(userId, request);
                return Ok(new { message = "Film and seats added to basket successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("getUserBasket")]
        public async Task<IActionResult> GetUserBasket()
        {
            int? userId = _userService.GetUserIdFromToken(HttpContext.User);
            if (userId == null)
            {
                return Unauthorized();
            }
            var basket = await _basketService.GetUserBasket(userId);
            if (basket == null)
            {
                return NotFound("Basket not found.");
            }
            return Ok(basket);
        }
    }
}
