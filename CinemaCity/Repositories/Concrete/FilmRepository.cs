using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Repositories.Concrete
{
    public class FilmRepository : IFilmRepository
    {
        private readonly CinemaCityDbContext _context;
        public FilmRepository(CinemaCityDbContext context)
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

        public async Task<Film> Get(int id)
        {
            return await _context.Films.FirstOrDefaultAsync(f=>f.Id == id);
        }

        public async Task<IEnumerable<Film>> GetAll()
        {
           return await _context.Films
                .Include(f=>f.FilmGenres)
                .ThenInclude(g=>g.Genre)
                .Include(f=>f.FilmActors)
                .Include(f=>f.Sessions)
                .ToListAsync();
        }

        public async Task Update(Film entity)
        {
            _context.Films.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
