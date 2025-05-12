using CinemaCity.Models;

namespace CinemaCity.Repositories.Abstract
{
    public interface IBasketRepository
    {
        Task<Basket?> GetUserBasketAsync(int? userId);
        Task<Basket> GetUserBasketWithTicketsAsync(int? userId);
        Task AddToBasketAsync(Basket basket);
        Task AddTicketAsync(Ticket ticket);
        Task SaveChangesAsync();
    }

}
