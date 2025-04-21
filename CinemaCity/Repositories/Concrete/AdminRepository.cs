using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Repositories.Concrete
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _context;
        public AdminRepository(AppDBContext context)
        {
            _context=context;
        }
        public async Task AddAsync(Admin entity)
        {
            await _context.Admins.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> GetAsync(string id)
        {
            return await _context.Admins.FirstOrDefaultAsync(f=>f.Id==id);
        }

        public async Task UpdateAsync(Admin entity)
        {
            _context.Admins.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
