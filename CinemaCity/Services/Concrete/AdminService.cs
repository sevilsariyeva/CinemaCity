using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;

namespace CinemaCity.Services.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task AddAsync(Admin entity)
        {
            await _adminRepository.AddAsync(entity);
        }

        public async Task<Admin> GetAsync(int id)
        {
            return await _adminRepository.GetAsync(id);
        }

        public async Task UpdateAsync(Admin entity)
        {
            await _adminRepository.UpdateAsync(entity);
        }
    }
}
