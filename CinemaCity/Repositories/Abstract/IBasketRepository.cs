using CinemaCity.Models;

namespace CinemaCity.Repositories.Abstract
{
    public interface IBasketRepository
    {
        Task<Basket> GetUserBasketAsync(int userId);
        Task CreateBasketAsync(Basket basket);
        Task SaveChangesAsync();
    }

}
