using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Repositories.Concrete
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDBContext _context;
        public FilmRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Film entity)
        {
            await _context.Films.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Film entity)
        {
            _context.Films.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Film> Get(string id)
        {
            return await _context.Films.FirstOrDefaultAsync(f=>f.Id == id);
        }

        public async Task<IEnumerable<Film>> GetAll()
        {
           return await _context.Films.ToListAsync();
        }

        public async Task Update(Film entity)
        {
            _context.Films.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
