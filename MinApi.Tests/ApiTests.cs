namespace MinApi.Tests
{
  public class ApiTests : IClassFixture<WebApplicationFactory<Dummy>>
  {
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
      Assert.True(response.Content.Headers.ContentLength > 0);
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
      new object[] { "/" },
      new object[] { "/healthcheck"},
      new object[] { "/quote" },
      new object[] { "/person"},
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
    public async Task Get_ReturnsCorrectResult()
    {
      var response = await _httpClient.GetAsync("/");

      response.EnsureSuccessStatusCode();
      Assert.Equal("Welcome to this Minimal API!", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Post_ReturnsCorrectReult()
    {
      var response = await _httpClient.PostAsync("/", JsonContent.Create(string.Empty));

      response.EnsureSuccessStatusCode();
      Assert.Equal("This is a minimal POST", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Put_ReturnsCorrectReult()
    {
      var response = await _httpClient.PutAsync("/", JsonContent.Create(string.Empty));

      response.EnsureSuccessStatusCode();
      Assert.Equal("This is a minimal PUT", await response.Content.ReadAsStringAsync());
    }


    [Fact]
    public async Task Delete_ReturnsCorrectReult()
    {
      var response = await _httpClient.DeleteAsync("/");

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
      var response = await _httpClient.PostAsync("/person", JsonContent.Create(p));

      response.EnsureSuccessStatusCode();
      Assert.Equal($"Welcome, {p.FirstName} {p.LastName}!", await response.Content.ReadAsStringAsync());
    }


    private bool IsFunny(string s) => s.EndsWith('.') || s.EndsWith('!') || s.EndsWith('?');

  }
}
