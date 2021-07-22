var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
  endpoints.MapGet("/", () => "Hello Minimal API!");
  endpoints.MapHealthChecks("/healthcheck");
  endpoints.MapGet("/person", () => new Person("Bill", "Gates"));
  endpoints.MapPost("/person", (Person p) => $"Welcome, {p.FirstName} {p.LastName}!");
});

await app.RunAsync();

public record Person(string FirstName, string LastName);