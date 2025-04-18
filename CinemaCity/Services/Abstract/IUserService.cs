using CinemaCity.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace CinemaCity.Services.Abstract
{
    public interface IUserService:IService<User>
    {
        Task<string> RegisterUserAsync(RegisterRequest request);
    }
}
