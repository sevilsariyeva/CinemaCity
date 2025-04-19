using CinemaCity.Models;
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
        var user = new User { Email = "user@gmail.com", Password = "User.123" };
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
    public async Task LoginUserAync_Success_ReturnJwtToken()
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Email = "user@gmail.com",
            Password = "User.123"
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
}
