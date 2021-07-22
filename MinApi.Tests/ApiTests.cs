using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace MinApi.Tests
{
  public class ApiTests : IClassFixture<WebApplicationFactory<Startup>>
  {
    private readonly HttpClient _httpClient;

    public ApiTests(WebApplicationFactory<Startup> factory)
    {
      _httpClient = factory.CreateDefaultClient();
    }

    [Fact]
    public async Task RootPath_ReturnsCorrectResult()
    {
      var response = await _httpClient.GetAsync("/");
      response.EnsureSuccessStatusCode();
      Assert.Equal("Hello Minimal API!", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task HealthCheck_ReturnsOK()
    {
      var response = await _httpClient.GetAsync("/healthcheck");
      response.EnsureSuccessStatusCode();
      Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetPerson_ReturnsPerson()
    {
      var response = await _httpClient.GetAsync("/person");

      response.EnsureSuccessStatusCode();
      Assert.True(response.Content.Headers.ContentLength > 0);
    }

    [Fact]
    public async Task PostPerson_ReturnsAck()
    {
      var response = await _httpClient.PostAsync("/person", JsonContent.Create(new Person("Don", "Knuth")));

      response.EnsureSuccessStatusCode();
      Assert.True(response.Content.Headers.ContentLength > 0);
    }
  }
}
