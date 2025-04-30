using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Repositories.Concrete
{
    public class BasketRepository:IBasketRepository
    {
        private readonly CinemaCityDbContext _context;
        public BasketRepository(CinemaCityDbContext context)
        {
            _context = context;
        }

        public async Task AddToBasketAsync(Basket basket)
        {
            await _context.Baskets.AddAsync(basket);
        }

        public async Task<Basket?> GetUserBasketAsync(int? userId)
        {
            return await _context.Baskets
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
