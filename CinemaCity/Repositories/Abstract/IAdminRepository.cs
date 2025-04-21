using CinemaCity.Models;

namespace CinemaCity.Repositories.Abstract
{
    public interface IAdminRepository
    {
        Task<Admin> GetAsync(string id);
        Task AddAsync(Admin entity);
        Task UpdateAsync(Admin entity);
    }
}
