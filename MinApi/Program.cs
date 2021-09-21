var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/hello", Hi);
app.MapGet("/", (LinkGenerator linker) => $"The link to the hello route is {linker.GetPathByName(nameof(Hi), values: null)}");
app.MapPost("/hello", () => "This is a minimal POST");
app.MapPut("/hello", () => "This is a minimal PUT");
app.MapDelete("/hello", () => "This is a minimal DELETE");

app.MapGet("/person", () => new Person("Edgar", "Knapp, Jr."));
app.MapPost("/person", (Person p) => $"Welcome, {p.FirstName} {p.LastName}!");
app.MapGet("/quote", async () => await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes"));

app.MapHealthChecks("/healthcheck");
app.MapSwagger();

await app.RunAsync();

string Hi() => "Welcome to this Minimal API!";

public record Person(string FirstName, string LastName);
