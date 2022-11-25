using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using src.Models;
using src.Models.Dtos;
using Xunit;

namespace reputationmanagement.be.web.IntegrationTests;

public class UserControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public UserControllerIntegrationTests(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();

    protected async Task AuthenticateAsync()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
    }

    private async Task<string> GetJwtAsync()
    {
        var response = await _client.PostAsJsonAsync("api/auth/sign_in", new UserLoginModel
        {
            Email = "testcustomer@example.com",
            Password = "Secret123$"
        });
       
        string result = await response.Content.ReadAsStringAsync();
        
        return result;

    }

    [Fact]
    public async Task ReturnPostedReviews_WhenCalled()
    {
        //Arrange
        await AuthenticateAsync();

        //Act
        var response = await _client.GetAsync("api/postedreviews");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ReturnHello_WhenCalled()
    {
        //Act
        var response = await _client.GetAsync("api/greet");

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateReview_WhenCalled()
    {
        //Arrange
        await AuthenticateAsync();

        //Act
        var response = await _client.PostAsJsonAsync("api/postreview", new ReviewForCreationDto
        {
            Email = "test@example.com",
            ReviewString = "Testing to see if this will work",
            Status = 0

        });

        //Assert
        response.EnsureSuccessStatusCode();
    }

}