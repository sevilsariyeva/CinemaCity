using CinemaCity.Models;

namespace CinemaCity.Repositories.Abstract
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
