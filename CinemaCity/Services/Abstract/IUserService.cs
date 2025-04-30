using System.Security.Claims;
using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace CinemaCity.Services.Abstract
{
    public interface IUserService:IService<User>
    {
        Task<string> RegisterUserAsync(RegisterUserRequestDTO request);
        Task<string> LoginUserAsync(LoginRequest request);
        int GetUserIdFromToken(ClaimsPrincipal user);
    }
}
