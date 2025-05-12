using System.Security.Claims;
using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IFilmService _filmService;

        public BasketService(IBasketRepository basketRepository, IFilmService filmService)
        {
            _basketRepository = basketRepository;
            _filmService = filmService;
        }

        public async Task<Basket> GetUserBasket(int? userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var basket = await _basketRepository.GetUserBasketWithTicketsAsync(userId);

            return basket;
        }


        public async Task AddTicketToBasketAsync(int? userId, AddToBasketRequestDTO request)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId), "User ID cannot be null");

            var films = await _filmService.GetAllAsync();
            var filmEntity = films.FirstOrDefault(f => f.Id == request.Film.Id);
            if (filmEntity == null)
                throw new InvalidOperationException("Film not found.");

            var basket = await _basketRepository.GetUserBasketAsync(userId);
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    TotalPrice = request.TotalPrice,
                    Tickets = new List<Ticket>()
                };

                await _basketRepository.AddToBasketAsync(basket);
            }

            foreach (var seat in request.Seats)
            {
                var ticket = new Ticket
                {
                    SeatRow = seat.Row,
                    SeatColumn = seat.Col,
                    SessionId = request.SessionId,
                    UserId = userId,
                    Basket = basket
                };

                basket.Tickets.Add(ticket);
            }

            basket.TotalPrice = request.TotalPrice; 
            await _basketRepository.SaveChangesAsync();
        }


        public int GetUserIdFromToken(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("nameid");
            if (userIdClaim == null)
            {
                throw new Exception("UserId not found in token");
            }

            return int.Parse(userIdClaim.Value);
        }

    }
}
