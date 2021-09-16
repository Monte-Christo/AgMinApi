var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Welcome to my Minimal API!");
app.MapPost("/", () => "This is a minimal POST");
app.MapPut("/", () => "This is a minimal PUT");
app.MapDelete("/", () => "This is a minimal DELETE");
app.MapHealthChecks("/healthcheck");
app.MapSwagger();
app.MapGet("/person", () => new Person("Bill", "Gates"));
app.MapPost("/person", (Person p) => $"Welcome, {p.FirstName} {p.LastName}!");
app.MapGet("/quote", async () => await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes"));

await app.RunAsync();

public record Person(string FirstName, string LastName);
