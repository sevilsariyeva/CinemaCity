using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Services.Concrete
{
    public class FilmService : IFilmService
    {
        private readonly IFilmRepository _filmRepository;
        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        public async Task Add(Film entity)
        {
            await _filmRepository.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var film = await _filmRepository.Get(id);
            if (film != null)
            {
                await _filmRepository.Delete(film);
            }
        }

        public async Task<Film> Get(int id)
        {
            return await _filmRepository.Get(id);
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _filmRepository.GetAll(); 
        }
        public async Task Update(Film entity)
        {
            await _filmRepository.Update(entity);
        }
    }
}
