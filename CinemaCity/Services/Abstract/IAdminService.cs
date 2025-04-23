using CinemaCity.Models;

namespace CinemaCity.Services.Abstract
{
    public interface IAdminService
    {
        Task<Admin> GetAsync(int id);
        Task AddAsync(Admin entity);
        Task UpdateAsync(Admin entity);
    }
}
