using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MinApi.Tests
{
  public class ApiTests : IClassFixture<WebApplicationFactory<Dummy>>
  {
    private const string HelloRoute = "/hello";
    private const string PersonRoute = "/person";

    private readonly HttpClient _httpClient;

    public ApiTests(WebApplicationFactory<Dummy> factory)
    {
      _httpClient = factory.CreateDefaultClient();
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
      var response = await _httpClient.GetAsync(PersonRoute);
      var p = new Person("Edgar", "Knapp");

      response.EnsureSuccessStatusCode();
      Assert.Equal(p, await response.Content.ReadFromJsonAsync<Person>());
    }

    [Fact]
    public async Task PostPerson_ReturnsAck()
    {
      var response = await _httpClient.PostAsync(PersonRoute, JsonContent.Create(new Person("Don", "Knuth")));

      response.EnsureSuccessStatusCode();
      Assert.True(response.Content.Headers.ContentLength > 0);
    }

    [Fact]
    public async Task SwaggerEndpoints_ReturnOk()
    {
      var response = await _httpClient.GetAsync("/swagger/index.html");
      response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task QuoteOfTheDay_ReturnsWebQuote()
    {
      var response = await _httpClient.GetAsync("/quote");

      response.EnsureSuccessStatusCode();
      Assert.True(response.Content.Headers.ContentLength > 0);

      var quotes = await response.Content.ReadFromJsonAsync<IList<string>>();
      Assert.All(quotes, q => Assert.True(IsFunny(q)));
    }

    public static IEnumerable<object[]> ValidUrls = new List<object[]>
    {
      new object[] { HelloRoute },
      new object[] { "/healthcheck"},
      new object[] { "/quote" },
      new object[] { PersonRoute},
      new object[] { "/swagger/index.html"},
      new object[] { "/swagger/v1/swagger.json" }
    };

    [Theory]
    [MemberData(nameof(ValidUrls))]
    public async Task GetValidUrls_ReturnsCorrectResult(string path)
    {
      var response = await _httpClient.GetAsync(path);
      response.EnsureSuccessStatusCode();
      Assert.True(response.Content.Headers.ContentLength > 0);
    }

    [Fact]
    public async Task GetRoot_ReturnsCorrectResult()
    {
      var response = await _httpClient.GetAsync("/");

      response.EnsureSuccessStatusCode();
      Assert.Equal("The root route is /hello", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetHello_ReturnsCorrectResult()
    {
      var response = await _httpClient.GetAsync(HelloRoute);

      response.EnsureSuccessStatusCode();
      Assert.Equal("Welcome to EK's Minimal API!", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostHello_ReturnsCorrectReult()
    {
      var response = await _httpClient.PostAsync(HelloRoute, JsonContent.Create(string.Empty));

      response.EnsureSuccessStatusCode();
      Assert.Equal("This is a minimal POST", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PutHello_ReturnsCorrectReult()
    {
      var response = await _httpClient.PutAsync(HelloRoute, JsonContent.Create(string.Empty));

      response.EnsureSuccessStatusCode();
      Assert.Equal("This is a minimal PUT", await response.Content.ReadAsStringAsync());
    }


    [Fact]
    public async Task DeleteHello_ReturnsCorrectReult()
    {
      var response = await _httpClient.DeleteAsync(HelloRoute);

      response.EnsureSuccessStatusCode();
      Assert.Equal("This is a minimal DELETE", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetHealth_ReturnsCorrectResult()
    {
      var response = await _httpClient.GetAsync("/healthcheck");

      response.EnsureSuccessStatusCode();
      Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostPerson_ReturnsCorrectReult()
    {
      var p = new Person("Albert", "Einstein");
      var response = await _httpClient.PostAsync(PersonRoute, JsonContent.Create(p));

      response.EnsureSuccessStatusCode();
      Assert.Equal($"Welcome, {p.FirstName} {p.LastName}!", await response.Content.ReadAsStringAsync());
    }


    private bool IsFunny(string s) => s.EndsWith('.') || s.EndsWith('!') || s.EndsWith('?');

  }
}
