namespace MinApi
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHealthChecks();
      services.AddSwaggerGen();
      services.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseRouting();
      app.UseSwagger();
      app.UseSwaggerUI();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGet("/", () => "Hello Minimal API!");
        endpoints.MapHealthChecks("/healthcheck");
        endpoints.MapGet("/person", () => new Person("Bill", "Gates"));
        endpoints.MapPost("/person", (Person p) => $"Welcome, {p.FirstName} {p.LastName}!");
        endpoints.MapSwagger();
        endpoints.MapGet("/quote", async () => await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes"));
      });
    }
  }
}