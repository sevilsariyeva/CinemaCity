using CinemaCity.Models;
using CinemaCity.Models.DTOs;

namespace CinemaCity.Services.Abstract
{
    public interface IBasketService
    {
        Task AddTicketToBasketAsync(int? userId, AddToBasketRequestDTO request);
        Task<Basket?> GetUserBasket(int? userId);
    }
}
