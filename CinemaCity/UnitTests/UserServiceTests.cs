using CinemaCity.Exceptions;
using CinemaCity.Models;
using CinemaCity.Models.DTOs;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Services.Abstract;
using CinemaCity.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Moq;
using Xunit;

namespace UnitTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly UserService _userService;
    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();
        _configurationMock = new Mock<IConfiguration>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _configurationMock.Object
        );
    }
    [Fact]
    public async Task LoginUserAsync_InvalidEmail_ThrowsUnauthorizedAccessException()
    {
        var loginRequest = new LoginRequest
        {
            Email = "system123@gmail.com",
            Password = "System.123"
        };
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.LoginUserAsync(loginRequest));
    }
    [Fact]
    public async Task LoginUserAsync_InvalidPassword_ThrowsUnAuthorizedException()
    {
        var user = new User { Email = "valid@email.com", Password = "hashedpassword" };
        var loginRequest = new LoginRequest
        {
            Email = user.Email,
            Password = "Wrong.123"
        };
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
        _passwordHasherMock.Setup(p => p.VerifyHashedPassword(user, user.Password, loginRequest.Password))
            .Returns(PasswordVerificationResult.Failed);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.LoginUserAsync(loginRequest));
    }
    [Fact]
    public async Task LoginUserAsync_Success_ReturnJwtToken()
    {
        var user = new User
        {
            Id = 111,
            Email = "valid@gmail.com",
            Password = "Password.123"
        };

        var loginRequest = new LoginRequest
        {
            Email = user.Email,
            Password = user.Password
        };
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
        _passwordHasherMock.Setup(p => p.VerifyHashedPassword(user, user.Password, loginRequest.Password))
            .Returns(PasswordVerificationResult.Success);

        _configurationMock.Setup(c => c["JwtSecret"]).Returns("SecretCode");

        var token = await _userService.LoginUserAsync(loginRequest);

        Assert.NotNull(token);
    }
    [Fact]
    public async Task RegisterUserAsync_InvalidEmail_ThrowsArgumentException()
    {
        var registerUser = new RegisterUserRequest
        {
            Email = "invalidEmail",
            Password = "Password.123",
            FullName = "Lisa Kudrow"
        };
        await Assert.ThrowsAsync<ArgumentException>(()=>_userService.RegisterUserAsync(registerUser));
    }
    [Fact]
    public async Task RegisterUserAsync_EmailAlreadyExists_ThrowsEmailAlreadyExistsException()
    {
        var registerRequest = new RegisterUserRequest
        {
            Email="sevilsariyeva00@gmail.com",
            Password="Sevil.123",
            FullName="Sevil Sariyeva"
        };
        var existingUser = new User { Email = registerRequest.Email };

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(registerRequest.Email)).ReturnsAsync(existingUser);

        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userService.RegisterUserAsync(registerRequest));
    }
    [Fact]
    public async Task RegisterUserAsync_EmailDoesNotExist_ThrowsEmailValidationException()
    {
        var registerRequest = new RegisterUserRequest
        {
            Email = "nonexistentcinemacity@gmail.com",
            Password = "Password1.23!",
            FullName = "Lisa Kudrow"
        };

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(registerRequest.Email)).ReturnsAsync((User)null);

        await Assert.ThrowsAsync<EmailValidationException>(() => _userService.RegisterUserAsync(registerRequest));
    }
    [Fact]
    public async Task RegisterUserAsync_Success_ReturnsJwtToken()
    {
        var registerRequest = new RegisterUserRequest
        {
            Email = "valid@gmail.com",
            Password = "Password.123",
            FullName = "Lisa Kudrow"
        };
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(registerRequest.Email)).ReturnsAsync((User)null);
        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), registerRequest.Password)).Returns("hashedpassword");

        _configurationMock.Setup(c => c["JwtSecret"]).Returns("SecretCode");

        var token = await _userService.RegisterUserAsync(registerRequest);

        Assert.NotNull(token);
    }
}
