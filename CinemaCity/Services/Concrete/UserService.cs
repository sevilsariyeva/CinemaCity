﻿using System.Linq.Expressions;
using CinemaCity.Exceptions;
using CinemaCity.Helpers;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;

namespace CinemaCity.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository,IPasswordHasher<User>passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }
        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }


        //public User Get(Expression<Func<User, bool>> expression)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterUserAsync(RegisterRequest request)
        {
            if (!await RegisterHelper.IsValidEmail(request.Email))
            {
                throw new ArgumentException("Invalid email format.", nameof(request.Email));
            }
            if (!await RegisterHelper.IsStrongPassword(request.Password))
            {
                throw new WeakPasswordException("Password must be at least 8 characters long and contain uppercase, lowercase, and a digit.");
            }
            var existingUser = _userRepository.GetUserByEmailAsync(request.Email);
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
                Email = request.Email,
                Password = request.Password,
                ImageUrl = "/uploads/upload_area.png"
            };
            newUser.Password=_passwordHasher.HashPassword(newUser,newUser.Password);
            await _userRepository.AddAsync(newUser);
            await EmailHelper.SendEmail(newUser.Email, "Welcome to Cinema City!","We are happy to see you! You should start to explore all films.");
            return JwtTokenGenerator.GenerateToken(newUser.Id.ToString(), newUser.Email, "User", _configuration);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
