var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello Minimal API!");
app.MapPost("/", () => "This is a POST");
app.MapPut("/", () => "This is a PUT");
app.MapDelete("/", () => "This is a DELETE");
app.MapHealthChecks("/healthcheck");
app.MapSwagger();
app.MapGet("/person", () => new Person("Bill", "Gates"));
app.MapPost("/person", (Person p) => $"Welcome, {p.FirstName} {p.LastName}!");
app.MapGet("/quote", async () => await new HttpClient().GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes"));

//app.Urls.Add("http://*:5030");
await app.RunAsync();

public record Person(string FirstName, string LastName);
