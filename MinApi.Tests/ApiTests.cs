using Microsoft.AspNetCore.Mvc.Testing;

namespace MinApi.Tests;

public class ApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
  private const string HelloRoute = "/hello";
  private const string PersonRoute = "/person";

  private readonly HttpClient _httpClient = factory.CreateDefaultClient();

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
    var p = new Person("Edgar", "Knapp", DateTime.Parse("June 21, 1959"));
    var response = await _httpClient.GetAsync(PersonRoute);
    response.EnsureSuccessStatusCode();
    Assert.Equal(p, await response.Content.ReadFromJsonAsync<Person>());
  }

  [Fact]
  public async Task PostPerson_ReturnsAck()
  {
    var response = await _httpClient.PostAsync(PersonRoute, JsonContent.Create(new Person("Don", "Knuth", new DateTime(1950, 2,3))));

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

    var quotes = await response.Content.ReadFromJsonAsync<IList<string>>() ?? new List<string>();
    Assert.All(quotes, q => Assert.True(IsFunny(q)));
  }

  public static readonly IEnumerable<object[]> ValidUrls = new List<object[]>
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
    Assert.Equal("Welcome to my Minimal API implementation!", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task PostHello_ReturnsCorrectResult()
  {
    var response = await _httpClient.PostAsync(HelloRoute, JsonContent.Create(string.Empty));

    response.EnsureSuccessStatusCode();
    Assert.Equal("This is a minimal POST", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task PutHello_ReturnsCorrectResult()
  {
    var response = await _httpClient.PutAsync(HelloRoute, JsonContent.Create(string.Empty));

    response.EnsureSuccessStatusCode();
    Assert.Equal("This is a minimal PUT", await response.Content.ReadAsStringAsync());
  }

  [Fact]
  public async Task DeleteHello_ReturnsCorrectResult()
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
  public async Task PostPerson_ReturnsCorrectResult()
  {
    //var timeProviderMock = new Mock<TimeProvider>();
    //timeProviderMock.Setup(tp => tp.GetUtcNow()).Returns(new DateTime(2023, 11, 7));
    //var bds = new BirthDayService(timeProviderMock.Object);
    //var check = bds.BirthdayCheck(DateTime.Parse("November 6, 1950"));

    var person = new Person("Albert", "Einstein", DateTime.Parse("November 8, 1950"));
    var birthdayCheck = DateTime.UtcNow.Month != person.BirthDate.Month || DateTime.UtcNow.Day != person.BirthDate.Day;
    var toBeOrNotToBe = birthdayCheck ? "not " : string.Empty;
    
  var response = await _httpClient.PostAsync(PersonRoute, JsonContent.Create(person));

    response.EnsureSuccessStatusCode();
    Assert.Equal($"Welcome, Albert Einstein! Today is {toBeOrNotToBe}your birthday", await response.Content.ReadAsStringAsync());
  }

  private static bool IsFunny(string s) => s.EndsWith('.') || s.EndsWith('!') || s.EndsWith('?');
}