﻿using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Services.Concrete
{
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task AddTicketToBasketAsync(int? userId, string seatNumber, int sessionId)
        {
            var basket = await _basketRepository.GetUserBasketAsync(userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    Tickets = new List<Ticket>()
                };

                await _basketRepository.AddToBasketAsync(basket);
            }

            if (basket.Tickets == null)
            {
                basket.Tickets = new List<Ticket>();
            }

            var ticket = new Ticket
            {
                SeatNumber = seatNumber,
                UserId = userId,
                SessionId = sessionId,
                BasketDetailId = basket.Id 
            };

            basket.Tickets.Add(ticket);

            await _basketRepository.SaveChangesAsync();
        }

    }
}
