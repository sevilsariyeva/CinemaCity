using System.Linq.Expressions;
using System.Security.Claims;
using CinemaCity.Exceptions;
using CinemaCity.Helpers;
using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Moq;

namespace CinemaCity.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public Task Add(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginUserAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return JwtTokenGenerator.GenerateToken(user.Id,user.Email,"User",_configuration);
        }

        public async Task<string> RegisterUserAsync(RegisterUserRequestDTO request)
        {
            if (!RegisterHelper.IsValidEmail(request.Email))
            {
                throw new ArgumentException("Invalid email format.", nameof(request.Email));
            }
            if (!RegisterHelper.IsStrongPassword(request.Password))
            {
                throw new WeakPasswordException("Password must be at least 8 characters long and contain uppercase, lowercase, and a digit.");
            }
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException("Email is already in use.");
            }
            if (!await EmailHelper.EmailExists(request.Email))
            {
                throw new EmailValidationException("Email address does not exist.");
            }
            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                ImageUrl = request.ImageUrl,
                CreatedAt= DateTime.UtcNow,
            };
            newUser.Password = _passwordHasher.HashPassword(newUser, newUser.Password);
            await _userRepository.AddAsync(newUser);
            await EmailHelper.SendEmail(newUser.Email, "Welcome to Cinema City!", "We are happy to see you! You should start to explore all films.");
            return JwtTokenGenerator.GenerateToken(newUser.Id, newUser.Email, "User", _configuration);
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
        public int GetUserIdFromToken(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ?? user.FindFirst("nameid");
            if (userIdClaim == null)
                throw new Exception("UserId not found in token");
            return int.Parse(userIdClaim.Value);
        }

    }
}
