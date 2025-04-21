using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Repositories.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
