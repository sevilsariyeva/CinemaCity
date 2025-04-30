using CinemaCity.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class UserControllerTests:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    public UserControllerTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }
    [Fact]
    public async Task Register_ReturnsToken_WhenValidRequest()
    {
        var request = new RegisterUserRequestDTO
        {
            FullName = "Test",
            Email = "test@example.com",
            Password = "Password.2025",
            ImageUrl= "https://www.pngmart.com/files/23/Profile-PNG-HD.png"
        };
        var response = await _httpClient.PostAsJsonAsync("/api/v1/user/register", request);

        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.True((bool)content.success);
        Assert.False(string.IsNullOrEmpty((string)content.token));
    }

}
